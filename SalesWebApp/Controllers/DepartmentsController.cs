using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebApp.Models;
using SalesWebApp.Models.ViewModels;
using SalesWebApp.Services;
using SalesWebApp.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

// DepartmentsController: Manages 'Department' CRUD operations.
// Depends on: 'DepartmentService' (Dependency Injection).

namespace SalesWebApp.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly DepartmentService _departmentService;

        public DepartmentsController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: Departments
        // Retrieves all Departments and returns them to the Views as a list.
        public async Task<IActionResult> Index()
        {
            var list = await _departmentService.FindAllAsync();
            return View(list);
        }

        // GET: Departments/Create
        // Prepares and returns the Departments creation form.
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // Receives the Department data submitted by the form.
        // If the model validation fails, returns the form again.
        // If successful, inserts the Department into the Database and redirects to Index (PRG pattern).
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            await _departmentService.InsertAsync(department);
            return RedirectToAction(nameof(Index));
        }

        // GET: Departments/Details/1
        // Retrieves and display details of the selected Department.
        // If it fails, redirects to Erros (id not provided or not found).
        // If successful, returns the View with the infos.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _departmentService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        // GET: Departments/Edit/1
        // Retrieves the selected Department and returns data through an edit form.
        // If it fails, redirects to Error (id not provided or not found).
        // If successful, returns the View with the edit form.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _departmentService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        // POST: Departments/Edit/1
        // Validates and updates Seller data in the Database.
        // If the model validation fails, returns the form again.
        // If successful, updates the Department in the Database and redirects to Index.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {

            if (!ModelState.IsValid)
            {
                return View(department);
            }
            if (id != department.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _departmentService.UpdateAsync(department);
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

        // GET: Departments/Delete/1
        // Retrieves the selected Department and returns a delete confirmation page.
        // If it fails, redirects to Error (id not provided or not found).
        // If successful, returns the View with the delete confirmation.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _departmentService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        // POST: Departments/Delete/1
        // Attempts to remove the selected Department from the Database.
        // If it fails, redirects to Error.
        // If successful, remove Department from the Database and redirects to Index.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _departmentService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
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
