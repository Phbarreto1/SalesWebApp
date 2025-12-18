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

        public SalesController(SalesRecordService salesRecordService, SellerService sellerService)
        {
            _salesRecordService = salesRecordService;
            _sellerService = sellerService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _salesRecordService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var sellers = await _sellerService.FindAllAsync();
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
        public async Task<IActionResult> Create(SalesRecord salesRecord)
        {
            if (!ModelState.IsValid)
            {
                var seller = await _sellerService.FindAllAsync();
                var status = Enum.GetValues(typeof(SaleStatus))
                .Cast<SaleStatus>()
                .Select(s => new SelectListItem
                {
                    Text = s.ToString(),
                    Value = s.ToString()
                });
                var viewModel = new SalesFormViewModel
                {
                    SalesRecord = salesRecord,
                    Sellers = seller,
                    StatusOfSale = status
                };
                return View(viewModel);
            }
            await _salesRecordService.InsertAsync(salesRecord);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _salesRecordService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _salesRecordService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _salesRecordService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _salesRecordService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            var sellers = await _sellerService.FindAllAsync();
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
        public async Task<IActionResult> Edit(int id, SalesRecord salesRecord)
        {
            if (!ModelState.IsValid)
            {
                var seller = await _sellerService.FindAllAsync();
                var status = Enum.GetValues(typeof(SaleStatus))
                .Cast<SaleStatus>()
                .Select(s => new SelectListItem
                {
                    Text = s.ToString(),
                    Value = s.ToString()
                });
                var viewModel = new SalesFormViewModel
                {
                    SalesRecord = salesRecord,
                    Sellers = seller,
                    StatusOfSale = status
                };
                return View(viewModel);
            }
            if (id != salesRecord.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _salesRecordService.UpdateAsync(salesRecord);
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
