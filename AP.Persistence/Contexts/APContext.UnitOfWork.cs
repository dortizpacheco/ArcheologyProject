using Microsoft.EntityFrameworkCore;
using AP.Domain.Models;

namespace AP.Persistence.Contexts
{
    public partial class APContext : DbContext
    {
        public DbSet<User> Users { get; set; }
       // public DbSet<Item> Items { get; set; }
       // public DbSet<Section> Sections { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
    }
}