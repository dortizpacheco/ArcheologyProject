using AP.Core.Dtos;
using AP.Core.Mapper;
using AP.Domain.Models;
using System.Collections.Generic;

namespace AP.Service.Mapper
{
    public class CatalogMapper : ICatalogMapper
    {      
        public Catalog UpdateElement(CatalogNewDto newItem, Catalog entity)
        {
            entity.Id = newItem.Id;
            entity.Name = newItem.Name;
            entity.FatherId = newItem.FatherId;
            return entity;
        }

        public CatalogDetailsDto GetDetails(Catalog e)  
        {
            List<CatalogSummaryDto> catalogs = new List<CatalogSummaryDto>();
            foreach(var subcatalog in e.InnerCatalogs)
            {
                catalogs.Add(GetSummary(subcatalog));
            }
            return  new CatalogDetailsDto()
            {
                Id = e .Id,
                Name = e.Name,
                InnerCatalogs = catalogs,
                Father = GetSummary(e.Father)
            };
        }

        public CatalogSummaryDto GetSummary(Catalog e) => new CatalogSummaryDto()
        {
            Id = e .Id,
            Name = e.Name
        };

        public Catalog NewElement(CatalogNewDto dto) => new Catalog()
        {
            Id = dto.Id,
            Name = dto.Name,
            FatherId = dto.FatherId
        };     
    }
}