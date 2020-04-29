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
    public class MyProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: MyProducts
        public async Task<ActionResult> Index()
        {

            var user = await GetCurrentUserAsync();
            var MyProducts = await _context.Product
                    .Where(p => p.UserId == user.Id)
                    .ToListAsync();

            return View(MyProducts);


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
private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}


