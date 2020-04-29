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
    [Route("api/[controller]")]
    [ApiController]
    public class LikeProductsController : ControllerBase
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<LikeProduct>> LikeProduct(int ProductId, bool liked)
        {
            var user = await GetCurrentUserAsync();
            var currentPreference = await _context.LikeProduct
                .FirstOrDefaultAsync(l => l.ProductId == ProductId && l.UserId == user.Id);
            if (currentPreference == null)
            {
                var likeProduct = new LikeProduct()
                {
                    UserId = user.Id,
                    ProductId = ProductId,
                    Like = liked
                };
                _context.LikeProduct.Add(likeProduct);
            } else {
                currentPreference.Like = liked;
                _context.LikeProduct.Add(currentPreference);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = ProductId });
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
