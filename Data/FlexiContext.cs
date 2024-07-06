using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class FlexiContext : IdentityDbContext<User>
{
    public DbSet<Instance> Instances { get; set; }
    public DbSet<Organization> Organisations { get; set; }

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