using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Valentine.Application.Interfaces;
using Valentine.Domain;

namespace Valentine.Persistence
{
    public class ValentineDbContext : DbContext, IValentineDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Map> Maps { get; set; }

        public DbSet<Area> Areas { get; set; }

        public DbSet<GeoPoint> GeoPoints { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("filename=Valentine.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>()
                .HasOne(a => a.Map)
                .WithMany(m => m.Areas)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GeoPoint>()
                .HasOne(p => p.Area)
                .WithMany(a => a.GeoPoints)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<File>()
                .HasOne(p => p.Area)
                .WithMany(a => a.Files)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public new DbSet<T> Set<T>() where T : File
        {
            return base.Set<T>();
        }
    }
}
