using BlogAPI.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AspNetUser, AspNetRole, int, AspNetUserClaim, 
        AspNetUserRole, AspNetUserLogin, AspNetRoleClaim, AspNetUserToken>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // RefreshToken
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.AspNetUser)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.ToTable("RefreshTokens");
            });

            // Menu
            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(rp => rp.Parent).WithMany(r => r.Children).HasForeignKey(rp => rp.ParentId).OnDelete(DeleteBehavior.Restrict);

                entity.ToTable("Menus");
            });

            // Permission
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(rp => rp.Menu).WithMany(r => r.Permissions).HasForeignKey(rp => rp.MenuId);

                entity.ToTable("Permissions");
            });

            // RolePermission
            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(rp => new { rp.RoleId, rp.PermissionId });

                entity.HasOne(rp => rp.Role).WithMany(r => r.RolePermissions).HasForeignKey(rp => rp.RoleId);
                entity.HasOne(rp => rp.Permission).WithMany(r => r.RolePermissions).HasForeignKey(rp => rp.PermissionId);

                entity.ToTable("RolePermissions");
            });
        }
    }
}
