using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Bangazon.Models.ProductViewModels;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices.ComTypes;
using System.IO;

namespace Bangazon.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Products
        public async Task<ActionResult> Index(string searchString)
        {
            //checking first if user input nothing or blank spaces, this returns EVERYTHING
            if (string.IsNullOrWhiteSpace(searchString))
            {
                var user = await GetCurrentUserAsync();
                var products = await _context.Product
                        //.Where(p => p.UserId == user.Id)
                        .Include(b => b.ProductType)
                        .ToListAsync();
                return View(products);
            }
            //checking now if the searchString matches an existing city in a product, 
            //helper method CityExists at bottom
            //If search string doesn't match a city, it searches by product Title
             else  if (!CityExists(searchString))
                {
                    var products = await _context.Product
                        .Include(p => p.ProductType)
                        .Include(p => p.User)
                        .Where(p => p.Title.Contains(searchString)).ToListAsync();

                    return View(products);
                }
            //If searchstring does match an existing city, it pulls all products matching that city.
            //Using Equals instead of Contains as the helper method requires a match, not a partial
                else
                {
                    var products = await _context.Product
                        .Include(p => p.ProductType)
                        .Include(p => p.User)
                        .Where(p => p.City.Equals(searchString)).ToListAsync();

                    return View(products);
                }
            }
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductType)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<ActionResult> Create()
        {
            var productTypeOptions = await _context.ProductType
                .Select(pt => new SelectListItem() { 
                    Text = pt.Label, 
                    Value = pt.ProductTypeId.ToString() 
                })
                .ToListAsync();

            var viewModel = new ProductFormViewModel();
            viewModel.ProductTypeOptions = productTypeOptions;

            return View(viewModel);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("ProductId,DateCreated,Description,Title,Price,Quantity,UserId,City,ImagePath,Active,ProductTypeId,File")] ProductFormViewModel productViewModel)
        {
            try
            {
                //gets the current user, uses custom method created at bottom
                //you will plug in the user.Id in the product
                var user = await GetCurrentUserAsync();

                //builds up our new product using the data submitted from the form, 
                //represented here as "productViewModel"
                var product = new Product
                {
                    ProductId = productViewModel.ProductId,
                    DateCreated = productViewModel.DateCreated,
                    Description = productViewModel.Description,
                    Title = productViewModel.Title,
                    Price = productViewModel.Price,
                    Quantity = productViewModel.Quantity,
                    UserId = user.Id,
                    City = productViewModel.City,
                    Active = productViewModel.Active,
                    ProductTypeId = productViewModel.ProductTypeId
                };
                if (productViewModel.File != null && productViewModel.File.Length > 0)
                {
                    //creates the file name
                    var fileName = Guid.NewGuid().ToString() + Path.GetFileName(productViewModel.File.FileName); 
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                    product.ImagePath = fileName;

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await productViewModel.File.CopyToAsync(stream);
                    }
                    
                }

                //adds the newly built product object to the Product table using _context.Product.Add
                _context.Product.Add(product);
                //You have to used SaveChangesAsync in order to actually submit the data to the database
                await _context.SaveChangesAsync();

                //returns user to the product Details view of the newly created product
                return RedirectToAction("Details", new { id = product.ProductId });
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "Label", product.ProductTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", product.UserId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,DateCreated,Description,Title,Price,Quantity,UserId,City,ImagePath,Active,ProductTypeId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "Label", product.ProductTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", product.UserId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductType)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(p => p.ProductId == id);
        }

        //basically copying the prebuilt ProductExists method but, this time checking for an existing City on a product
        private bool CityExists(string city)
        {
            return _context.Product.Any(p => p.City.Equals(city));
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
