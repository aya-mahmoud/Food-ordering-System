#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewProj.Models;
using NewProj.Classes;
using Microsoft.AspNetCore.Authorization;
using NewProj.Models.Interfaces;

namespace NewProj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private IOrderRepository Order_Repostory;
        private ICartRepository Cart_Repostory;

        public OrdersController(IOrderRepository r, ICartRepository r2)
        {
            Order_Repostory = r;
            Cart_Repostory = r2;
        }

        // GET: Admin/Orders
        

        public async Task<IActionResult> Index()
        {
            var projectContext = Order_Repostory.GetOrders();
            return View(await projectContext.ToListAsync());
        }

        // GET: Admin/Orders/Details/5
        

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var prod = Order_Repostory.GetOrders()
              .Include(a => a.Products)
               .SingleOrDefault(a => a.ID == id);

            var order = await Order_Repostory.GetOrders().FirstOrDefaultAsync(m => m.ID == id);



            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Admin/Orders/Create
        //public IActionResult Create()
        //{
        //    ViewData["CartID"] = new SelectList(_context.Carts, "ID", "ID");
        //    return View();
        //}

        //// POST: Admin/Orders/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,TotalPrice,Status,CartID")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(order);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CartID"] = new SelectList(_context.Carts, "ID", "ID", order.CartID);
        //    return View(order);
        //}

        // GET: Admin/Orders/Edit/5
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await Order_Repostory.GetOrders().FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            //ViewData["CartID"] = new SelectList(Cart_Repostory.GetCarts(), "ID", "ID", order.CartID);
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        

        public async Task<IActionResult> Edit(int id, [Bind("ID,TotalPrice,Status,CartID")] Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Order_Repostory.Update(order);
                    await Order_Repostory.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.ID))
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
            //ViewData["CartID"] = new SelectList(Cart_Repostory.GetCarts(), "ID", "ID", order.CartID);
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await Order_Repostory.GetOrders().FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await Order_Repostory.GetOrders().FindAsync(id);
            Order_Repostory.Delete(order);
            await Order_Repostory.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return Order_Repostory.GetOrders().Any(e => e.ID == id);
        }
    }
}
