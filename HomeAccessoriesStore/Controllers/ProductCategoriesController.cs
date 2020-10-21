using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeAccessoriesStore.Data;
using HomeAccessoriesStore.Models;

namespace HomeAccessoriesStore.Controllers
{
    public class ProductCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductCategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductCategories.Include(p => p.Products);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategories = await _context.ProductCategories
                .Include(p => p.Products)
                .FirstOrDefaultAsync(m => m.ProductCategoriesId == id);
            if (productCategories == null)
            {
                return NotFound();
            }

            return View(productCategories);
        }

        // GET: ProductCategories/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name");
            return View();
        }

        // POST: ProductCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductCategoriesId,ProductId,ProductName")] ProductCategories productCategories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productCategories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productCategories.ProductId);
            return View(productCategories);
        }

        // GET: ProductCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategories = await _context.ProductCategories.FindAsync(id);
            if (productCategories == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productCategories.ProductId);
            return View(productCategories);
        }

        // POST: ProductCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductCategoriesId,ProductId,ProductName")] ProductCategories productCategories)
        {
            if (id != productCategories.ProductCategoriesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoriesExists(productCategories.ProductCategoriesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productCategories.ProductId);
            return View(productCategories);
        }

        // GET: ProductCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategories = await _context.ProductCategories
                .Include(p => p.Products)
                .FirstOrDefaultAsync(m => m.ProductCategoriesId == id);
            if (productCategories == null)
            {
                return NotFound();
            }

            return View(productCategories);
        }

        // POST: ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productCategories = await _context.ProductCategories.FindAsync(id);
            _context.ProductCategories.Remove(productCategories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoriesExists(int id)
        {
            return _context.ProductCategories.Any(e => e.ProductCategoriesId == id);
        }
    }
}
