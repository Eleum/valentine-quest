using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using Valentine.Domain;

namespace Valentine.Persistence
{
    public class ValentineDbContext : DbContext
    {
        public DbSet<Area> Areas { get; set; }

        public DbSet<Map> Maps { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("filename=Valentine.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
    }
}
