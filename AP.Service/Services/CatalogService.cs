using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AP.Core.Dtos;
using AP.Core.Services;
using AP.Persistence.Contexts;
using AP.Domain.Models;
using System.Collections;
using System.Collections.Generic;
using AP.Core.Mapper;
using System.Linq;

namespace AP.Service
{
    public class CatalogService : ICatalogService
    {
        APContext _context;
        ICatalogMapper _mapper;

        public CatalogService(APContext context,ICatalogMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<dynamic> CreateAsync(CatalogNewDto newItem)
        {
            var entity = _mapper.NewElement(newItem);
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new { Id = entity.Id };
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _context.Catalogs.FindAsync(id);

            if (result == null ) return false;

            _context.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<CatalogSummaryDto>> GetAll(string fatherId)
        {
            var list = await _context.Catalogs.Where(s => s.FatherId == fatherId)
                                                 .AsNoTracking()
                                                 .ToListAsync();
            return list.ConvertAll(s => _mapper.GetSummary(s));
        }

        public async Task<CatalogDetailsDto> GetByIdAsync(string id)
        {
            var entity = await _context.Catalogs.FindAsync(id);
            if(entity == null) return null;

            await _context.Entry(entity).Navigation("InnerCatalogs").LoadAsync();
            await _context.Entry(entity).Navigation("Father").LoadAsync();
            await _context.Entry(entity).Navigation("Items").LoadAsync();

            return _mapper.GetDetails(entity);
        }

        public async Task<dynamic> UpdateByIdAsync(string id,CatalogNewDto newItem)
        {
            var entity = await _context.Catalogs.FindAsync(id);
            if(entity == null) return null;
            
            _context.Update(_mapper.UpdateElement(newItem,entity));
            await _context.SaveChangesAsync();

            return new { Id = id };
        }
    }
}