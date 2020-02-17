using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Valentine.Domain;

namespace Valentine.Application.Interfaces
{
    public interface IValentineDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Map> Maps { get; set; }

        public DbSet<Area> Areas { get; set; }

        public DbSet<GeoPoint> GeoPoints { get; set; }

        public DbSet<Image> Images { get; set; }

        Task<int> SaveChangesAsync();
    }
}
