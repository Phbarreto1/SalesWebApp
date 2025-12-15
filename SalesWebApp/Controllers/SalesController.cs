using Microsoft.AspNetCore.Mvc;
using SalesWebApp.Models;
using SalesWebApp.Services;

namespace SalesWebApp.Controllers
{
    public class SalesController : Controller
    {
        private readonly SalesService _salesService;

        public SalesController(SalesService salesService)
        {
            _salesService = salesService;
        }

        public IActionResult Index()
        {
            var list = _salesService.FindAll();
            return View(list);
        }
    }
}
