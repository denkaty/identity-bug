
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AuthTest
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<StaffUser> StaffUsers { get; set; }
        public DbSet<StaffRole> StaffRoles { get; set; }
        public DbSet<StaffUserClaim> StaffUserClaims { get; set; }
        public DbSet<StaffUserLogin> StaffUserLogins { get; set; }
        public DbSet<StaffUserRole> StaffUserRoles { get; set; }
        public DbSet<StaffUserToken> StaffUserTokens { get; set; }
        public DbSet<StaffRoleClaim> StaffRoleClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // StaffRoles
            builder.Entity<StaffRole>(entity =>
            {
                entity.ToTable(name: "StaffRoles");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(256);
                entity.Property(e => e.NormalizedName).HasMaxLength(256);
                entity.HasIndex(e => e.NormalizedName).HasName("RoleNameIndex").IsUnique();
            });

            // StaffUsers
            builder.Entity<StaffUser>(entity =>
            {
                entity.ToTable(name: "StaffUsers");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserName).HasMaxLength(256);
                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
                entity.Property(e => e.Email).HasMaxLength(256);
                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
                entity.HasIndex(e => e.NormalizedEmail).HasName("EmailIndex");
                entity.HasIndex(e => e.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            });

            // AspNetRoleClaims
            builder.Entity<StaffRoleClaim>(entity =>
            {
                entity.ToTable(name: "StaffRoleClaims");
                entity.HasKey(e => e.Id);
                entity.HasOne<StaffRole>().WithMany().HasForeignKey(e => e.RoleId).IsRequired();
            });

            // StaffUserClaims
            builder.Entity<StaffUserClaim>(entity =>
            {
                entity.ToTable(name: "StaffUserClaims");
                entity.HasKey(e => e.Id);
                entity.HasOne<StaffUser>().WithMany().HasForeignKey(e => e.UserId).IsRequired();
            });

            // StaffUserLogins
            builder.Entity<StaffUserLogin>(entity =>
            {
                entity.ToTable(name: "StaffUserLogins");
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
                entity.HasOne<StaffUser>().WithMany().HasForeignKey(e => e.UserId).IsRequired();
            });

            // StaffUserRoles
            builder.Entity<StaffUserRole>(entity =>
            {
                entity.ToTable(name: "StaffUserRoles");
                entity.HasKey(e => new { e.UserId, e.RoleId });
                entity.HasOne<StaffUser>().WithMany().HasForeignKey(e => e.UserId).IsRequired();
                entity.HasOne<StaffRole>().WithMany().HasForeignKey(e => e.RoleId).IsRequired();
            });

            // StaffUserTokens
            builder.Entity<StaffUserToken>(entity =>
            {
                entity.ToTable(name: "StaffUserTokens");
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
                entity.HasOne<StaffUser>().WithMany().HasForeignKey(e => e.UserId).IsRequired();
            });
        }
    }
}
