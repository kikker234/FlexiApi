using Data.Models;
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
}