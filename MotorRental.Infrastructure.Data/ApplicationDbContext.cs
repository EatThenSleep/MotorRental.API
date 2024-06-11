using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotorRental.Entities;
using MotorRental.Utilities;
using System.Reflection.Metadata;


namespace MotorRental.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Motorbike> Motorbikes { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Surcharge> Surcharges { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedData(builder, "Admin 1", "admin@gmail.com", SD.ADMIN);
            SeedData(builder, "Owner 1", "owner@gmail.com", SD.OWNER);
            SeedData(builder, "Visitor 1", "visitor@gmail.com", SD.VISTOR);

        }

        private void SeedData(ModelBuilder builder, string name , string email, string role)
        {
            var adminRoleId = Guid.NewGuid().ToString();

            // Create Reader And Writer Role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = adminRoleId,
                    Name = role,
                    NormalizedName = role.ToLower(),
                    ConcurrencyStamp = adminRoleId
                },
            };

            // Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            // Create an Admin User
            var adminUserId = Guid.NewGuid().ToString();
            var admin = new User()
            {
                Id = adminUserId,
                Name = name,
                UserName = email,
                Email = email,
                NormalizedEmail = email.ToUpper(),
                NormalizedUserName = name.ToUpper(),
            };
            admin.PasswordHash = new PasswordHasher<User>().HashPassword(admin, "Cong&Vien0504");
            builder.Entity<User>().HasData(admin);
            // Give role to Admin User
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId.ToString(),
                    RoleId = adminRoleId
                },
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
