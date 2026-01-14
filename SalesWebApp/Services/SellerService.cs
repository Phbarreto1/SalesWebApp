using Microsoft.EntityFrameworkCore;
using SalesWebApp.Models;
using SalesWebApp.Services.Exceptions;

// SellerService: Manages data operations for 'Seller' using EF Core (DbContext).
// Depends on: 'SalesWebAppContext' (Dependency Injection).

namespace SalesWebApp.Services
{
    public class SellerService
    {
        private readonly SalesWebAppContext _context;

        public SellerService(SalesWebAppContext context) {
            _context = context;
        }

        // Returns a List of Sellers from the Database.
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        // Insert a new Seller (object) into the Database.
        public async Task InsertAsync(Seller obj) {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        // Returns from the Database a selected Seller by id.
        // Includes the related Department of the Seller, otherwise Department would return null.
        public async Task<Seller> FindByIdAsync(int id) {
            return await _context.Seller
                .Include(obj => obj.Department)
                .FirstOrDefaultAsync(obj => obj.Id == id);
        }

        // Attempts to update an existing Seller from the Database.
        // If the id doesn't exist, throws NotFoundException.
        // If there is any conflict, throws DbConcurrencyException.
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        // Attempts to remove from the Database a selected Seller by id.
        // If it fails, throw an Exception.
        // If successful, locate the Seller (by id) and then removes it from the Database.
        // Note: if the id doesn't exist, obj may be null.
        public async Task RemoveAsync(int id) {
            try 
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) {
                throw new IntegrityException(e.Message);
            }
        }
    }
}
