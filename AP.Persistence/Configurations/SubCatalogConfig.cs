using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AP.Domain.Models;

namespace AP.Persistence.Configurations
{
    public class  CatalogConfig : IEntityTypeConfiguration<Catalog>
    {
        public void Configure(EntityTypeBuilder<Catalog> builder)
        {
            builder.Property(s => s.Id).IsRequired();
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.FatherId);
    
            builder.HasKey(s => s.Id);
            
            builder.HasOne(s => s.Father)
                   .WithMany(f => f.InnerCatalogs)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasForeignKey(s => s.FatherId);
        }
    }
}