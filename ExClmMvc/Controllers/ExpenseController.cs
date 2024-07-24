using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ExClmMvc.Data;
using ExClmMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExClmMvc.Controllers
{
    [Route("[controller]")]
    public class ExpenseController : Controller
    {
        private readonly Context context;

        public ExpenseController(Context _context)
        {
            context = _context;
        }
        // GET: Expense/Index
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var claims = await context.ExpenseClaims
                                      .Include(e => e.Employee)
                                      .Include(e => e.Category)
                                      .ToListAsync();

            foreach (var claim in claims)
            {
                var subcategoryIds = claim.SubcategoryIds?.Split(',').Select(id => int.Parse(id)).ToList() ?? new List<int>();
                var subcategoryNames = context.ExpenseSubcategories
                    .Where(s => subcategoryIds.Contains(s.SubcategoryId))
                    .Select(s => s.SubcategoryName)
                    .ToList();

                claim.SubcategoryNames = string.Join(", ", subcategoryNames);
            }

            return View(claims);
        }


        // GET: Expense/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(context.ExpenseCategories, "CategoryId", "CategoryName");
            ViewData["Employees"] = new SelectList(context.Employees, "EmployeeId", "Name");
            return View();
        }

        // POST: Expense/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,CategoryId,SubcategoryIds,ClaimAmount,ExpenseDate,ExpenseLocation,BillAttachment,Remarks")] ExpenseClaim model)
        {
            if (ModelState.IsValid)
            {
                var employee = await context.Employees.FindAsync(model.EmployeeId);
                if (employee != null)
                {
                    switch (employee.Designation)
                    {
                        case "CEO":
                            if (model.ClaimAmount > 10000)
                                ModelState.AddModelError("ClaimAmount", "Claim amount exceeds the limit for CEO.");
                            break;
                        case "Manager":
                            if (model.ClaimAmount > 7000)
                                ModelState.AddModelError("ClaimAmount", "Claim amount exceeds the limit for Manager.");
                            break;
                        default:
                            if (model.ClaimAmount > 5000)
                                ModelState.AddModelError("ClaimAmount", "Claim amount exceeds the limit for employees.");
                            break;
                    }

                    if (!ModelState.IsValid)
                    {
                        ViewData["Categories"] = new SelectList(context.ExpenseCategories, "CategoryId", "CategoryName");
                        ViewData["Employees"] = new SelectList(context.Employees, "EmployeeId", "Name");
                        return View(model);
                    }

                    // Ensure SubcategoryIds is a comma-separated string
                    model.SubcategoryIds = string.Join(",", Request.Form["SubcategoryIds"].ToArray());

                    context.Add(model);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["Categories"] = new SelectList(context.ExpenseCategories, "CategoryId", "CategoryName");
            ViewData["Employees"] = new SelectList(context.Employees, "EmployeeId", "Name");
            return View(model);
        }

        // AJAX: GetSubcategories
        [HttpGet("GetSubcategories")]
        public JsonResult GetSubcategories(int categoryId)
        {
            var subcategories = context.ExpenseSubcategories
                .Where(s => s.CategoryId == categoryId)
                .Select(s => new { s.SubcategoryId, s.SubcategoryName })
                .ToList();
            return Json(subcategories);
        }
    }
}