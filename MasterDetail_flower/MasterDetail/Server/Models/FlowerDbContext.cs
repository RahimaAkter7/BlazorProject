using MasterDetail.Shared;
using Microsoft.EntityFrameworkCore;

namespace MasterDetail.Server.Models
{
    public class FlowerDbContext : DbContext
    {
        public FlowerDbContext(DbContextOptions<FlowerDbContext> options) : base(options) {}

        public DbSet<Flower> Flowers { get; set; } = default!;
        public DbSet<BouquetType> BouquetTypes { get; set; } = default!;
        public DbSet<StoreEntry> StoreEntries { get; set; } = default!;
    }
}
