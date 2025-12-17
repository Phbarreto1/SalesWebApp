using Microsoft.EntityFrameworkCore;
using SalesWebApp.Models;

namespace SalesWebApp.Services
{
    public class SalesService
    {
        private readonly SalesWebAppContext _context;

        public SalesService (SalesWebAppContext context)
        {
            _context = context;
        }

        public List<SalesRecord> FindAll()
        {
            return _context.SalesRecords
                .Include(sr => sr.Seller)
                .ToList();
        }

        public SalesRecord FindById(int id)
        {
            return _context.SalesRecords.Include(obj => obj.Seller).FirstOrDefault(obj => obj.Id == id);

        }
    }
}
