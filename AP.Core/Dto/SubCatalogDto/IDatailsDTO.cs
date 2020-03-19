using System.Collections.Generic;

namespace AP.Core.Dtos
{
    public class CatalogDetailsDto
    {
        public object Id { get; set; }
        public string Name { get; set; }
        public ICollection<CatalogSummaryDto> InnerCatalogs { get; set; }   
        public CatalogSummaryDto Father { get; set; } 
        //public ICollection<ItemSummaryDto> Items { get; set; }
    }
}