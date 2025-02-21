using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

/// <summary>
/// This code snippet was provided by Sebastian Hult, a tip for getting a unique Project Number.
/// </summary>
public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CustomerContactEntity> CustomerContacts { get; set; } = null!;
    public DbSet<CustomerEntity> Customers { get; set; } = null!;
    public DbSet<EmployeeEntity> Employees { get; set; } = null!;
    public DbSet<ProjectEntity> Projects { get; set; } = null!;
    public DbSet<RoleEntity> Roles { get; set; } =null!;
    public DbSet<ServiceEntity> Services { get; set; } = null!;
    public DbSet<StatusEntity> Status { get; set; } = null!;
    public DbSet<UnitEntity> Unit { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProjectEntity>()
            .Property(p => p.Id)
            .UseIdentityColumn(100, 1);

    }

}
