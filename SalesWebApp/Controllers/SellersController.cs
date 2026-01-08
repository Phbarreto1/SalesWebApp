using Microsoft.AspNetCore.Mvc;
using SalesWebApp.Models;
using SalesWebApp.Models.ViewModels;
using SalesWebApp.Services;
using SalesWebApp.Services.Exceptions;
using System.Diagnostics;

// SellersController: Manages 'Seller' CRUD operations.
// Depends on: 'SellerService' and 'DepartmentService' (dependency injection).

// SellerFormViewModel:
// Is used to group 'Seller' data and the 'Departments' collection,
// allowing the View to build a 'Department' selection dropdown.

namespace SalesWebApp.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        // GET: Sellers
        // Retrieves all Sellers and returns them to the View as a list.
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        // GET: Sellers/Create
        // Prepares and returns the Sellers creation form.
        // Includes an ICollection of Departments (rendered in a selection dropdown)
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel 
            { 
                Departments = departments 
            };
            return View(viewModel);
        }

        // POST: Sellers/Create
        // Receives the Seller data submitted by the form.
        // If the model validation fails, reloads Departments and returns the form again.
        // If successful, inserts the Seller into the Database and redirects to Index (PRG pattern).
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel
                {
                    Seller = seller,
                    Departments = departments
                };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        // GET: Sellers/Details/1
        // Retrieves and displays details of the selected Seller.
        // If it fails, redirects to Error (id not provided or not found).
        // If successful, returns the View with the infos.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        // GET: Sellers/Edit/1
        // Retrieves the selected Seller and returns data through an edit form.
        // If it fails, redirects to Error (id not provided or not found).
        // If successful, returns the View with the edit form.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel
            {
                Seller = obj,
                Departments = departments
            };
            return View(viewModel);
        }

        // POST: Sellers/Edit/1
        // Validates and updates Seller data in the Database.
        // If the model validation fails, reloads Departments and returns the form again.
        // If successful, updates the Seller in the Database and redirects to Index.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel
                {
                    Seller = seller,
                    Departments = departments
                };
                return View(viewModel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
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

        // GET: Sellers/Delete/1
        // Retrieves the selected Seller and returns a delete confirmation page.
        // If it fails, redirects to Error (id not provided or not found).
        // If successful, returns the View with the delete confirmation.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        // POST: Sellers/Delete/1
        // Removes the selected Seller from the Database.
        // If it fails, redirects to Error.
        // If successful, remove Seller from the Database and redirects to Index.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
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
