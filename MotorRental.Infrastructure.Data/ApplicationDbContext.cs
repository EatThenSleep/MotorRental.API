using Microsoft.EntityFrameworkCore;
using MotorRental.Entities;
using System.Reflection.Metadata;


namespace MotorRental.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Motorbike> Motorbikes { get; set; }

        
    }
}
