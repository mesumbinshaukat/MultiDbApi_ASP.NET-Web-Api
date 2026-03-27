using Microsoft.EntityFrameworkCore;

namespace MultiDbApi
{
    public class ElasticPoolDbContext : DbContext
    {
        public ElasticPoolDbContext(DbContextOptions<ElasticPoolDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Further configuration can go here
        }
    }
}
