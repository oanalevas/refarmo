using Microsoft.EntityFrameworkCore;

namespace refarmo.Models
{
    public class RNTSContext : DbContext
    {
        public RNTSContext(DbContextOptions options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Feature> Feature { get; set; }
        public DbSet<FeatureCollection> FeatureCollection { get; set; }
    }
}
