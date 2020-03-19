using System.Threading.Tasks;
using AP.Core.Dtos;
using System.Collections.Generic;

namespace AP.Core.Services
{
    public interface ICatalogService
    {
        Task<CatalogDetailsDto> GetByIdAsync(string id);
        Task<dynamic> CreateAsync(CatalogNewDto newItem);
        Task<bool> DeleteAsync(string id);
        Task<ICollection<CatalogSummaryDto>> GetAll(string fatherId);
        Task<dynamic> UpdateByIdAsync(string id ,CatalogNewDto newItem);
    }
}