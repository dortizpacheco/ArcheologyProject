using System.Collections.Generic;
using System;
using AP.Domain.Models;
using AP.Core.Dtos;

namespace AP.Core.Mapper
{
    public interface ICatalogMapper
    {
        Catalog NewElement(CatalogNewDto dto);
        CatalogSummaryDto GetSummary(Catalog e);
        CatalogDetailsDto GetDetails(Catalog e);
        Catalog UpdateElement(CatalogNewDto newItem, Catalog entity);
    }
}