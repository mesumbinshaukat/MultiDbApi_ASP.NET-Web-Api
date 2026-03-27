using Microsoft.EntityFrameworkCore;
using MultiDbApi.Models;

namespace MultiDbApi
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Map TableInfo as a keyless entity for raw SQL queries
            modelBuilder.Entity<TableInfo>().HasNoKey();
        }
    }
}
