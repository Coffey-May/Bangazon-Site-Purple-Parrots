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
                    TypeId = pt.ProductTypeId,
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
                    TypeId = pt.ProductTypeId,
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

    }
}
