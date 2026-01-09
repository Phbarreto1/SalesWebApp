using SalesWebApp.Models;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

        // Insert a new Department (object) into the Database.
        public async Task InsertAsync(Department obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }


    }
}
