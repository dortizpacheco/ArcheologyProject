using Microsoft.EntityFrameworkCore;
using AP.Persistence.Configurations;

namespace AP.Persistence.Contexts
{
    public partial class APContext : DbContext
    {
        public APContext(DbContextOptions<APContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new ItemConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());

            // modelBuilder.ApplyConfiguration(new SectionConfig());
            modelBuilder.ApplyConfiguration(new CatalogConfig());
        }
    }
}