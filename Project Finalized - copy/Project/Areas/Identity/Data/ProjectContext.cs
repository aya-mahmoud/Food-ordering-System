using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data;

public class ProjectContext : IdentityDbContext<IdentityUser>
{
    public ProjectContext(DbContextOptions<ProjectContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Project.Models.Product> Product { get; set; }

    public DbSet<Project.Models.Category> Category { get; set; }

    public DbSet<Project.Models.Order> Order { get; set; }

    public DbSet<Project.Models.Cart> Cart { get; set; }

    public DbSet<Project.Models.User> User { get; set; }
}
