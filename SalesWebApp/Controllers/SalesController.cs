using Microsoft.AspNetCore.Mvc;
using SalesWebApp.Models;
using SalesWebApp.Services;

namespace SalesWebApp.Controllers
{
    public class SalesController : Controller
    {
        private readonly SalesService _salesService;
        private readonly SellerService _sellerService;

        public SalesController(SalesService salesService, SellerService sellerService)
        {
            _salesService = salesService;
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _salesService.FindAll();
            return View(list);
        }
    }
}
