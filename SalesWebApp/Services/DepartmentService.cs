using Microsoft.EntityFrameworkCore;
using SalesWebApp.Models;
using SalesWebApp.Services.Exceptions;

// DepartmentService: Manages data operations for 'Department' using EF Core (DbContext).
// Depends on: 'SalesWebAppContext' (Dependency Injection).

namespace SalesWebApp.Services
{
    public class DepartmentService
    {
        private readonly SalesWebAppContext _context;

        public DepartmentService(SalesWebAppContext context)
        {
            _context = context;
        }

        // Returns a List of Departments from the Database.
        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Id).ToListAsync();
        }

        // Insert a new Department (object) into the Database.
        public async Task InsertAsync(Department obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        // Returns from the Database a selected Seller by id.
        public async Task<Department> FindByIdAsync(int id)
        {
            return await _context.Department
                .FirstOrDefaultAsync (obj => obj.Id == id);
        }

        // Attempts to update an existing Department from the Database.
        // If the id doesn't exist, throws NotFoundException.
        // If there is any conflict, throws DbConcurrencyException.
        public async Task UpdateAsync(Department obj)
        {
            bool hasAny = await _context.Department.AnyAsync(x => x.Id == obj.Id);
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

        // Attempts to remove from the Database a selected Department by id.
        // If it fails, throw and Exception.
        // If successful, locate the Department (by id) and then removes it from the Database.
        // Note: if the id doesn't exist, obj may be null.
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Department.FindAsync(id);
                _context.Department.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
    }
}
