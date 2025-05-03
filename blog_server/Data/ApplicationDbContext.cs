using blog_server.Constants;
using blog_server.Helpers;
using blog_server.Models;
using blog_server.Sessions;
using Microsoft.EntityFrameworkCore;

namespace blog_server.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    ICurrentUser currentUser
) : DbContext(options)
{
    private readonly ICurrentUser _currentUser = currentUser;
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder
            .Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder
            .Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

        modelBuilder.Entity<Category>(entity =>
        {
            entity
                .HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey(e => e.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(modelBuilder);

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed roles
        modelBuilder
            .Entity<Role>()
            .HasData(
                new Role
                {
                    Id = 1,
                    Name = "ADMIN",
                    Description = "Administrator role",
                    CreateDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                },
                new Role
                {
                    Id = 2,
                    Name = "USER",
                    Description = "User role",
                    CreateDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                }
            );
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e =>
                e.Entity is BaseEntity
                && (e.State == EntityState.Added || e.State == EntityState.Modified)
            );

        foreach (var entityEntry in entries)
        {
            var entity = (BaseEntity)entityEntry.Entity;

            if (entityEntry.State == EntityState.Added)
            {
                entity.CreateDate = DateTime.UtcNow;
                entity.CreateBy = _currentUser.UserId;
            }
            else
            {
                entityEntry.Property("CreateDate").IsModified = false;
                entityEntry.Property("CreateBy").IsModified = false;
            }

            entity.UpdateDate = DateTime.UtcNow;
            entity.UpdateBy = _currentUser.UserId;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
