using System;
using blog_server.Constants;
using blog_server.Exceptions;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().Property(r => r.Name).HasConversion<string>();

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

        base.OnModelCreating(modelBuilder);

        SeedData(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreateDate = DateTime.UtcNow;
                entry.Entity.UpdateDate = DateTime.UtcNow;
                entry.Entity.CreateBy ??= GetCurrentUserId();
                entry.Entity.UpdateBy ??= GetCurrentUserId();
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdateDate = DateTime.UtcNow;
                entry.Entity.UpdateBy = GetCurrentUserId();
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    private Guid GetCurrentUserId()
    {
        return _currentUser.UserId
            ?? throw new ApiException(
                "User is not authenticated",
                StatusCodes.Status401Unauthorized
            );
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        var adminRole = new Role
        {
            Id = 1,
            Name = AppRole.ADMIN,
            Description = "Administrator role",
            CreateDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow,
            CreateBy = null,
            UpdateBy = null,
        };
        var userRole = new Role
        {
            Id = 2,
            Name = AppRole.USER,
            Description = "User role",
            CreateDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow,
            CreateBy = null,
            UpdateBy = null,
        };

        modelBuilder.Entity<Role>().HasData(adminRole, userRole);

        var userId1 = new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98");
        var userId2 = new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87");

        var user1 = new User
        {
            Id = userId1,
            Username = "admin",
            Email = "admin@example.com",
            PasswordHash = PasswordHelper.HashPassword("admin123"),
            Status = AppStatus.Active,
            CreateDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow,
            CreateBy = null,
            UpdateBy = null,
        };
        var user2 = new User
        {
            Id = userId2,
            Username = "user",
            Email = "user@example.com",
            PasswordHash = PasswordHelper.HashPassword("user123"),
            Status = AppStatus.Active,
            CreateDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow,
            CreateBy = null,
            UpdateBy = null,
        };

        modelBuilder.Entity<User>().HasData(user1, user2);

        modelBuilder
            .Entity<UserRole>()
            .HasData(
                new UserRole
                {
                    UserId = userId1,
                    RoleId = 1,
                    JoinDate = DateTime.UtcNow,
                },
                new UserRole
                {
                    UserId = userId2,
                    RoleId = 2,
                    JoinDate = DateTime.UtcNow,
                }
            );
    }
}
