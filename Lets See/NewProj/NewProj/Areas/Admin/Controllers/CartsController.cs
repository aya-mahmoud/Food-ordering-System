#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewProj.Models;
using NewProj.Models.Interfaces;

namespace NewProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CartsController : Controller
    {
        private  ICartRepository Cart_Repostory;

        public CartsController( ICartRepository r)
        {
  
            Cart_Repostory = r;
        }

        // GET: Admin/Carts
        //public async Task<IActionResult> Index()
        //{
        //    var projectContext = _context.Carts.Include(c => c.User);
        //    return View(await projectContext.ToListAsync());
        //}

        // GET: Admin/Carts/Details/5

        

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await Cart_Repostory.GetCarts()
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        

        // GET: Admin/Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await Cart_Repostory.GetCarts()
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Admin/Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await Cart_Repostory.GetCarts().FindAsync(id);
            Cart_Repostory.Delete(cart);
            await Cart_Repostory.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return Cart_Repostory.GetCarts().Any(e => e.ID == id);
        }
    }
}
