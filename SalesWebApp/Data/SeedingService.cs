using SalesWebApp.Models;
using SalesWebApp.Models.Enums;

namespace SalesWebApp.Data
{
    public class SeedingService
    {
        private SalesWebAppContext _context;

        public SeedingService(SalesWebAppContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Department.Any() ||
                _context.Seller.Any() ||
                _context.SalesRecords.Any())
            {
                return; // Database has been seeded
            }

            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Phones");
            Department d3 = new Department(3, "Tablets");
            Department d4 = new Department(4, "Games");
            Department d5 = new Department(5, "TVs");
            Department d6 = new Department(6, "Cameras");
            Department d7 = new Department(7, "Books");
            Department d8 = new Department(8, "Toys");

            Seller s1 = new Seller(1, "Juan Carlos", "juancarlos@email.com", new DateTime(1995, 5, 15), 2000.0, d1);
            Seller s2 = new Seller(2, "Ana Maria", "anamaria@email.com", new DateTime(1980, 10, 8), 3500.0, d2);
            Seller s3 = new Seller(3, "Pedro Grey", "pedrogrey@email.com", new DateTime(1990, 1, 2), 2200.0, d1);
            Seller s4 = new Seller(4, "Rose Brown", "rosebrown@email.com", new DateTime(1993, 8, 9), 3000.0, d3);
            Seller s5 = new Seller(5, "John Feld", "johnfeld@email.com", new DateTime(1995, 2, 20), 4000.0, d4);
            Seller s6 = new Seller(6, "Vilma Lind", "vilmalind@email.com", new DateTime(1975, 1, 1), 3200.0, d7);
            Seller s7 = new Seller(7, "Ferdinand Ned", "ferdinandned@email.com", new DateTime(1999, 10, 15), 2900.0, d4);
            Seller s8 = new Seller(8, "Hillary Jan", "hillaryjan@email.com", new DateTime(1995, 5, 15), 2100.0, d5);
            Seller s9 = new Seller(9, "Joana Bade", "joanabade@email.com", new DateTime(1993, 2, 13), 2000.0, d6);
            Seller s10 = new Seller(10, "Henry Ted", "henryted@email.com", new DateTime(1991, 3, 14), 3100.0, d8);

            SalesRecord r1 = new SalesRecord(1, new DateTime(2025, 11, 3), 11000.0, SaleStatus.Billed, s1);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2025, 11, 5), 7000.0, SaleStatus.Billed, s7);
            SalesRecord r3 = new SalesRecord(3, new DateTime(2025, 11, 7), 4000.0, SaleStatus.Canceled, s4);
            SalesRecord r4 = new SalesRecord(4, new DateTime(2025, 11, 9), 8000.0, SaleStatus.Billed, s3);
            SalesRecord r5 = new SalesRecord(5, new DateTime(2025, 11, 10), 3000.0, SaleStatus.Billed, s3);
            SalesRecord r6 = new SalesRecord(6, new DateTime(2025, 11, 12), 2000.0, SaleStatus.Billed, s8);
            SalesRecord r7 = new SalesRecord(7, new DateTime(2025, 11, 15), 13000.0, SaleStatus.Billed, s1);
            SalesRecord r8 = new SalesRecord(8, new DateTime(2025, 11, 22), 400.0, SaleStatus.Billed, s6);
            SalesRecord r9 = new SalesRecord(9, new DateTime(2025, 11, 28), 11000.0, SaleStatus.Pending, s2);
            SalesRecord r10 = new SalesRecord(10, new DateTime(2025, 11, 28), 9000.0, SaleStatus.Billed, s5);
            SalesRecord r11 = new SalesRecord(11, new DateTime(2025, 11, 29), 600.0, SaleStatus.Billed, s10);
            SalesRecord r12 = new SalesRecord(12, new DateTime(2025, 11, 30), 7000.0, SaleStatus.Pending, s3);
            SalesRecord r13 = new SalesRecord(13, new DateTime(2025, 11, 30), 10000.0, SaleStatus.Billed, s4);
            SalesRecord r14 = new SalesRecord(14, new DateTime(2025, 12, 1), 3000.0, SaleStatus.Billed, s5);
            SalesRecord r15 = new SalesRecord(15, new DateTime(2025, 12, 1), 400.0, SaleStatus.Billed, s10);
            SalesRecord r16 = new SalesRecord(16, new DateTime(2025, 12, 1), 2000.0, SaleStatus.Billed, s4);
            SalesRecord r17 = new SalesRecord(17, new DateTime(2025, 12, 2), 12000.0, SaleStatus.Billed, s8);
            SalesRecord r18 = new SalesRecord(18, new DateTime(2025, 12, 3), 6000.0, SaleStatus.Billed, s8);
            SalesRecord r19 = new SalesRecord(19, new DateTime(2025, 12, 4), 800.0, SaleStatus.Billed, s6);
            SalesRecord r20 = new SalesRecord(20, new DateTime(2025, 12, 4), 8000.0, SaleStatus.Billed, s6);
            SalesRecord r21 = new SalesRecord(21, new DateTime(2025, 12, 4), 9000.0, SaleStatus.Billed, s2);
            SalesRecord r22 = new SalesRecord(22, new DateTime(2025, 12, 5), 4000.0, SaleStatus.Billed, s4);
            SalesRecord r23 = new SalesRecord(23, new DateTime(2025, 12, 6), 11000.0, SaleStatus.Canceled, s2);
            SalesRecord r24 = new SalesRecord(24, new DateTime(2025, 12, 7), 8000.0, SaleStatus.Billed, s9);
            SalesRecord r25 = new SalesRecord(25, new DateTime(2025, 12, 7), 7000.0, SaleStatus.Billed, s7);
            SalesRecord r26 = new SalesRecord(26, new DateTime(2025, 12, 7), 5000.0, SaleStatus.Billed, s6);
            SalesRecord r27 = new SalesRecord(27, new DateTime(2025, 12, 8), 9000.0, SaleStatus.Pending, s1);
            SalesRecord r28 = new SalesRecord(28, new DateTime(2025, 12, 8), 4000.0, SaleStatus.Billed, s3);
            SalesRecord r29 = new SalesRecord(29, new DateTime(2025, 12, 9), 12000.0, SaleStatus.Billed, s5);
            SalesRecord r30 = new SalesRecord(30, new DateTime(2025, 12, 9), 500.0, SaleStatus.Billed, s6);
            SalesRecord r31 = new SalesRecord(31, new DateTime(2025, 12, 9), 7000.0, SaleStatus.Billed, s2);
            SalesRecord r32 = new SalesRecord(32, new DateTime(2025, 12, 9), 5000.0, SaleStatus.Billed, s6);
            SalesRecord r33 = new SalesRecord(33, new DateTime(2025, 12, 10), 9000.0, SaleStatus.Pending, s1);
            SalesRecord r34 = new SalesRecord(34, new DateTime(2025, 12, 10), 4000.0, SaleStatus.Billed, s10);
            SalesRecord r35 = new SalesRecord(35, new DateTime(2025, 12, 11), 1200.0, SaleStatus.Billed, s6);
            SalesRecord r36 = new SalesRecord(36, new DateTime(2025, 12, 11), 500.0, SaleStatus.Billed, s6);

            _context.Department.AddRange(d1, d2, d3, d4, d5, d6, d7, d8);

            _context.Seller.AddRange(s1, s2, s3, s4, s5, s6, s7, s8, s9, s10);

            _context.SalesRecords.AddRange(
                r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16,
                r17, r18, r19, r20, r21, r22, r23, r24, r25, r26, r27, r28, r29, r30, 
                r31, r32, r33, r34, r35, r36
            );

            _context.SaveChanges();
        }

    }
}
