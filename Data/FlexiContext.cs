using Data.Models;
using Data.Models.components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class FlexiContext : IdentityDbContext<User>
{
    public DbSet<Instance> Instances { get; set; }
    public DbSet<Organization> Organisations { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Customer> Customers { get; set; }
    
    public DbSet<Component> Components { get; set; }
    public DbSet<ComponentField> ComponentFields { get; set; }
    public DbSet<ComponentData> ComponentData { get; set; }
    public DbSet<ComponentValidation> ComponentValidations { get; set; }

    public FlexiContext(DbContextOptions<FlexiContext> options) : base(options)
    {
    }

    protected FlexiContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}