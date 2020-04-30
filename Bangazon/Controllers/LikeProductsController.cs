using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Identity;

namespace Bangazon.Controllers
{
    public class LikeProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LikeProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/LikeProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeProduct>>> GetLikeProduct()
        {
            return await _context.LikeProduct.ToListAsync();
        }

        // GET: api/LikeProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LikeProduct>> GetLikeProduct(int id)
        {
            var likeProduct = await _context.LikeProduct.FindAsync(id);

            if (likeProduct == null)
            {
                return NotFound();
            }

            return likeProduct;
        }

       

        // PUT: api/LikeProducts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLikeProduct(int id, LikeProduct likeProduct)
        {
            if (id != likeProduct.LikeId)
            {
                return BadRequest();
            }

            _context.Entry(likeProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikeProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LikeProducts
        [HttpPost]
        public async Task<ActionResult> LikePreference(Product product)
        {
            var user = await GetCurrentUserAsync();
            var currentPreference = await _context.LikeProduct
                .FirstOrDefaultAsync(l => l.ProductId == product.ProductId && l.UserId == user.Id);
            if (currentPreference == null)
            {
                var likeProduct = new LikeProduct()
                {
                    UserId = user.Id,
                    ProductId = product.ProductId,
                    Like = true
                };
                _context.LikeProduct.Add(likeProduct);
            } else if (currentPreference.Like == false) 
            {
                currentPreference.Like = true;
                _context.LikeProduct.Update(currentPreference);
            }
            else
            {
                _context.LikeProduct.Remove(currentPreference);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Products", new { id = product.ProductId });
        }

        // POST: api/LikeProducts
        [HttpPost]
        public async Task<ActionResult> DisLikePreference(Product product)
        {
            var user = await GetCurrentUserAsync();
            var currentPreference = await _context.LikeProduct
                .FirstOrDefaultAsync(l => l.ProductId == product.ProductId && l.UserId == user.Id);
            if (currentPreference == null)
            {
                var likeProduct = new LikeProduct()
                {
                    UserId = user.Id,
                    ProductId = product.ProductId,
                    Like = false
                };
                _context.LikeProduct.Add(likeProduct);
            }
            else if (currentPreference.Like == false)
            {
                currentPreference.Like = true;
                _context.LikeProduct.Update(currentPreference);
            }
            else
            {
                _context.LikeProduct.Remove(currentPreference);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Products", new { id = product.ProductId });
        }

        // DELETE: api/LikeProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LikeProduct>> DeleteLikeProduct(int id)
        {
            var likeProduct = await _context.LikeProduct.FindAsync(id);
            if (likeProduct == null)
            {
                return NotFound();
            }

            _context.LikeProduct.Remove(likeProduct);
            await _context.SaveChangesAsync();

            return likeProduct;
        }

        private bool LikeProductExists(int id)
        {
            return _context.LikeProduct.Any(e => e.LikeId == id);
        }
    private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
