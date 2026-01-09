using System.ComponentModel.DataAnnotations;

namespace SalesWebApp.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "{0} size should have between {2} and {1} characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double BaseSalary { get; set; }

        // Navigation property: many Sellers belong to one Department
        public Department Department { get; set; }

        // Foreign key for Department
        public int DepartmentId { get; set; }

        // Collection of Sales associated with the Seller
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        // Adds a sales record to the Seller
        public void AddSales(SalesRecord record)
        {
            Sales.Add(record);
        }

        // Removes a sales record from the Seller
        public void RemoveSales(SalesRecord record)
        {
            Sales.Remove(record);
        }

        // Calculates the total amount of sales within a date range
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(record => record.Date >= initial && record.Date <= final).Sum(record => record.Amount);
        }
    }
}
