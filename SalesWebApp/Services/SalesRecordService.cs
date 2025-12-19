using Microsoft.EntityFrameworkCore;
using SalesWebApp.Models;
using SalesWebApp.Services.Exceptions;
using System.Threading.Tasks;

namespace SalesWebApp.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebAppContext _context;

        public SalesRecordService (SalesWebAppContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindAllAsync()
        {
            return await _context.SalesRecords
                .Include(sr => sr.Seller)
                .OrderBy(sr => sr.Date.Month)
                .ThenBy(sr => sr.Date.Day)
                .ToListAsync();
        }

        public async Task<SalesRecord> FindByIdAsync(int id)
        {
            return await _context.SalesRecords
                .Include(obj => obj.Seller)
                .FirstOrDefaultAsync(obj => obj.Id == id);

        }

        public async Task InsertAsync(SalesRecord obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.SalesRecords.FindAsync(id);
            _context.SalesRecords.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SalesRecord obj)
        {
            bool hasAny = await _context.SalesRecords.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
