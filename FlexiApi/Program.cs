using System.ComponentModel;
using System.Text;
using Auth;
using Auth.Attributes;
using Business;
using Business.Services;
using Data;
using Data.Models;
using Data.Repositories;
using FlexiApi.Attributes;
using FlexiApi.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FlexiContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    if (connectionString is null)
    {
        throw new InvalidEnumArgumentException("Connection string not found");
    }

    ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
    options.UseMySql(connectionString, serverVersion);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddIdentity<User, IdentityRole>(options => 
    {
        // Configure Identity options here
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<FlexiContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IAuthManager, AuthManager>();

builder.Services.AddScoped<DbContext, FlexiContext>();

builder.Services.AddScoped<InstanceServices>();
builder.Services.AddScoped<InstanceRepository>();

builder.Services.AddScoped<CustomerServices>();

builder.Services.AddScoped<OrganizationServices>();
builder.Services.AddScoped<OrganizationRepository>();

builder.Services.AddScoped<CreateOrganizationValidator>();
builder.Services.AddScoped<CreateOrganizationValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

string key = builder.Configuration["Jwt:Key"];
string issuer = builder.Configuration["Jwt:Issuer"];
string audience = builder.Configuration["Jwt:Audience"];

TokenUtils.SecretKey = key;
TokenUtils.Issuer = issuer;
TokenUtils.Audience = audience;

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

var supportedCultures = new[] { "en", "nl" };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<AuthorizeActionFilter>();
    options.Filters.Add<ValidationActionFilter>();
});

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}

        }
    });
});

var app = builder.Build();

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();