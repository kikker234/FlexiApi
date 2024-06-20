using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class FlexiContext : IdentityDbContext<User>
{
    /* API */
    public DbSet<Instance> Instances { get; set; }
    public DbSet<Organization> Organizations { get; set; }

    public FlexiContext(DbContextOptions<FlexiContext> options) : base(options)
    {
    }

    protected FlexiContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Instance instance = new Instance
        {
            Id = 1,
            Name = "Flexi",
            Description = "Flexi is a flexible and scalable platform for managing your organization's data",
            Key = Guid.NewGuid().ToString(),
        };

        Organization organization = new Organization
        {
            Id = 1,
            Name = "Flexi",
            InstanceId = 1,
        };

        User user = new User
        {
            Id = "1",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@admin.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
            OrganizationId = 1,
        };
        
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "password");

        modelBuilder.Entity<Instance>().HasData(instance);
        modelBuilder.Entity<Organization>().HasData(organization);
        modelBuilder.Entity<User>().HasData(user);
    }
}