using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class FlexiContext : IdentityDbContext<User>
{
    /* FEATURE FLAGS */
    public DbSet<FeatureFlag> FeatureFlags { get; set; }
    
    /* API */
    public DbSet<Instance> Instances { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    
    public FlexiContext(DbContextOptions<FlexiContext> options) : base(options)
    {
    }
    protected FlexiContext() { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        var user = new User { 
            Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
            Email = "admin@admin.com",
            EmailConfirmed = true, 
            UserName = "admin",
            NormalizedUserName = "ADMIN"
        };

        PasswordHasher<User> ph = new PasswordHasher<User>();
        user.PasswordHash = ph.HashPassword(user, "password");

        builder.Entity<User>().HasData(user);
    }
}