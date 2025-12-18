using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;
using SalesWebApp.Models;
using SalesWebApp.Models.Enums;
using SalesWebApp.Models.ViewModels;
using SalesWebApp.Services;
using SalesWebApp.Services.Exceptions;
using System.Diagnostics;

namespace SalesWebApp.Controllers
{
    public class SalesController : Controller
    {
        private readonly SalesRecordService _salesRecordService;
        private readonly SellerService _sellerService;

        public SalesController(SalesRecordService salesService, SellerService sellerService)
        {
            _salesRecordService = salesService;
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _salesRecordService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var sellers = _sellerService.FindAll();
            var status = Enum.GetValues(typeof(SaleStatus))
                .Cast<SaleStatus>()
                .Select(s => new SelectListItem
                {
                    Text = s.ToString(),
                    Value = s.ToString()
                });


            var viewModel = new SalesFormViewModel
            {
                Sellers = sellers,
                StatusOfSale = status,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SalesRecord salesRecord)
        {
            _salesRecordService.Insert(salesRecord);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _salesRecordService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _salesRecordService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _salesRecordService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _salesRecordService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            var sellers = _sellerService.FindAll();
            var status = Enum.GetValues(typeof(SaleStatus))
                .Cast<SaleStatus>()
                .Select(s => new SelectListItem
                {
                    Text = s.ToString(),
                    Value = s.ToString()
                });

            SalesFormViewModel viewModel = new SalesFormViewModel 
            { 
                SalesRecord = obj,
                Sellers = sellers,
                StatusOfSale = status
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SalesRecord salesRecord)
        {
            if (id != salesRecord.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                _salesRecordService.Update(salesRecord);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }



        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
