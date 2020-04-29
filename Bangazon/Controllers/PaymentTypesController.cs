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

namespace bangazon.controllers
{
    [Authorize]
    public class PaymentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;

        public PaymentTypesController(ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _usermanager = usermanager;
        }

        // get: paymenttypes
        public async Task<ActionResult> Index()
        {
            var user = await getcurrentuserasync();
            var paymenttypes = await _context.PaymentType
                .Where(pt => pt.UserId == user.Id)
                .ToListAsync();
            return View(paymenttypes);
        }

        // get: paymenttypes/details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // get: paymenttypes/create
        public ActionResult Create()
        {
            return View();
        }

        // post: paymenttypes/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PaymentType paymenttype)
        {
            try
            {
                var user = await getcurrentuserasync();

                var paymenttypeinstance = new PaymentType
                {
                    Description = paymenttype.Description,
                    AccountNumber = paymenttype.AccountNumber,
                };

                paymenttypeinstance.UserId = user.Id;


                _context.PaymentType.Add(paymenttypeinstance);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // get: paymenttypes/edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // post: paymenttypes/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // todo: add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // get: paymenttypes/delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var paymenttype = await _context.PaymentType.FirstOrDefaultAsync(pt => pt.PaymentTypeId == id);

            var loggedinuser = await getcurrentuserasync();

            if (paymenttype.UserId != loggedinuser.Id)
            {
                return NotFound();
            }

            return View(paymenttype);
        }

        // post: paymenttypes/delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, PaymentType paymenttype)
        {
            try
            {

                _context.PaymentType.Remove(paymenttype);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private Task<ApplicationUser> getcurrentuserasync() => _usermanager.GetUserAsync(HttpContext.User);
    }
}