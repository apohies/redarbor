using Microsoft.EntityFrameworkCore;
using Redarbor.Domain.Entities;

namespace Redarbor.Infrastructure.Context;

public class RedarborDbContext : DbContext
{
    public RedarborDbContext(DbContextOptions<RedarborDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CompanyId)
                .IsRequired();

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.PortalId)
                .IsRequired();

            entity.Property(e => e.RoleId)
                .IsRequired();

            entity.Property(e => e.StatusId)
                .IsRequired();

            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Name)
                .HasMaxLength(100);

            entity.Property(e => e.Fax)
                .HasMaxLength(50);

            entity.Property(e => e.Telephone)
                .HasMaxLength(50);

            entity.HasIndex(e => e.Email)
                .IsUnique();

            entity.HasIndex(e => e.Username)
                .IsUnique();
        });
    }
}