using System.ComponentModel;
using System.Text;
using Business;
using Business.Services;
using Data;
using Data.Models;
using Data.Repositories;
using FeatureFlags;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

builder.Services.AddScoped<DbContext, FlexiContext>();
builder.Services.AddScoped<OrganizationServices>();
builder.Services.AddScoped<OrganizationRepository>();

builder.Services.AddScoped<FeatureFlagRepository>();
builder.Services.AddScoped<FeatureFlagManager>();

builder.Services.AddScoped<InstanceServices>();
builder.Services.AddScoped<InstanceRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string key = builder.Configuration["Jwt:Key"];
string issuer = builder.Configuration["Jwt:Issuer"];
string audience = builder.Configuration["Jwt:Audience"];

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

builder.Services.AddControllers(options => { options.Filters.Add<FeatureActionFilter>(); });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();