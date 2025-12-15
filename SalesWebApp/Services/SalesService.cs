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
            return _context.SalesRecords.ToList();
        }

    }
}
