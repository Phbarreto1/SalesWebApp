using Microsoft.AspNetCore.Mvc.Rendering;
using SalesWebApp.Models.Enums;

namespace SalesWebApp.Models.ViewModels
{
    public class SalesFormViewModel
    {
        public SalesRecord SalesRecord { get; set; }

        public IEnumerable<SelectListItem> StatusOfSale { get; set; }

        public ICollection<Seller> Sellers { get; set; }

    }
}
