using Microsoft.EntityFrameworkCore;
using SalesWebApp.Models;
using SalesWebApp.Services.Exceptions;

namespace SalesWebApp.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebAppContext _context;

        public SalesRecordService (SalesWebAppContext context)
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

        public void Insert(SalesRecord obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var obj = _context.SalesRecords.Find(id);
            _context.SalesRecords.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(SalesRecord obj)
        {
            if (!_context.SalesRecords.Any(x => x.Id == obj.Id))
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
