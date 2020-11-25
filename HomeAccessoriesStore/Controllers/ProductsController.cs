using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeAccessoriesStore.Data;
using HomeAccessoriesStore.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace HomeAccessoriesStore.Controllers
{
    //make it private
    [Authorize(Roles = "Administrator")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Product.Include(p => p.Category);
            return View(await applicationDbContext.OrderBy(p =>p.Name).ToListAsync());
        }
        //allow to use everyone
        [AllowAnonymous]

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category.OrderBy(c =>c.Name), "CategoryId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Discription,Price,Brand,Stock,CategoryId")] Products products, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                //we want to check there is a photo and then upload the photo
                if (Photo.Length>0)
                {
                    //get a temp location of the upload file
                    var tempFile = Path.GetTempFileName();
                    //Create a unique name using the Unique Id Guid class
                    var myFileName = Guid.NewGuid() + "-" + Photo.FileName;
                    //Set the destination dyanmic path and file name
                    var uploadPath = System.IO.Directory.GetCurrentDirectory() + "/wwwroot/img/pics/" + myFileName;
                    //use a stream to create anew file
                    using (var stream = new FileStream(uploadPath, FileMode.Create)) 
                    await Photo.CopyToAsync(stream);
                    //Add unique file name as the photo property of new obj
                    products.Photo = myFileName;

                }
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", products.CategoryId);
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Product.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category.OrderBy(c =>c.Name), "CategoryId", "Name", products.CategoryId);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Discription,Price,Brand,Stock,CategoryId")] Products products,IFormFile Photo)
        {
            if (id != products.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    //we want to check there is a photo and then upload the photo
                    if (Photo.Length > 0)
                    {
                        //get a temp location of the upload file
                        var tempFile = Path.GetTempFileName();
                        //Create a unique name using the Unique Id Guid class
                        var myFileName = Guid.NewGuid() + "-" + Photo.FileName;
                        //Set the destination dyanmic path and file name
                        var uploadPath = System.IO.Directory.GetCurrentDirectory() + "/wwwroot/img/pics/" + myFileName;
                        //use a stream to create anew file
                        using (var stream = new FileStream(uploadPath, FileMode.Create))
                            await Photo.CopyToAsync(stream);
                        //Add unique file name as the photo property of new obj
                        products.Photo = myFileName;

                    }
                    
    

                        _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", products.CategoryId);
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Product.FindAsync(id);
            _context.Product.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
   
}

