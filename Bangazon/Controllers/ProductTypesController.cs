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
using Bangazon.Models.ProductTypeViewModels;
using System.Runtime.CompilerServices;

namespace Bangazon.Controllers
{
    [Authorize]
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ProductTypes
        public async Task<ActionResult> Index()
        {

            var products = await _context.ProductType
                .Include(p => p.Products)
                .Select(pt => new ProductList()
                {
                    Id = pt.ProductTypeId,
                    Name = pt.Label,
                    ProductCount = pt.Products.Count(),
                    Products = pt.Products.OrderByDescending(p => p.DateCreated).Take(3)
                }).ToListAsync();


            return View(products);
        }

        // GET: ProductTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType
                .Include(pt => pt.Products)
                .Select(pt => new ProductList()
                {
                    Id = pt.ProductTypeId,
                    Name = pt.Label,
                    ProductCount = pt.Products.Count(),
                    Products = pt.Products.Where(p => p.ProductTypeId == id)
                }).ToListAsync();

            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);

        }

        // GET: ProductTypes/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductTypeId,Label")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(productType);

        }

        // GET: ProductTypes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductTypes/Edit/5

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }


            return View(productType);

        }

        // POST: ProductTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("ProductType,Label")] ProductType productType)
        {
            if (id != productType.ProductTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypeExists(productType.ProductTypeId))
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
            return View(productType);
        }



        // GET: ProductTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType
                .FirstOrDefaultAsync(m => m.ProductTypeId == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);

        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var productType = await _context.ProductType.FindAsync(id);
            _context.ProductType.Remove(productType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
         private bool ProductTypeExists(int id)
            {
                return _context.ProductType.Any(e => e.ProductTypeId == id);
            }
        }

    } 
