using System.Data.Common;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

public class AppContext : DbContext
{
    // This class is currently empty, but it can be used to manage database connections and operations in the future.
    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
    }
    public DbSet<Models.Backend.User> Users { get; set; }

    // protected override void OnModelCreated(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Models.Backend.User>()
    //         .HasIndex(u => u.Email)
    //         .IsUnique();
    // }
}