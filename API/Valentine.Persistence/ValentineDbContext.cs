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
        public DbSet<Area> Areas { get; set; }

        public DbSet<Map> Maps { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("filename=Valentine.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
