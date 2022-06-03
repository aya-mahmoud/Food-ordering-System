#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Project.Controllers
{
    public class CartsController : Controller
    {
        private readonly ProjectContext _context;

        public CartsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var projectContext = _context.Cart.Include(c => c.User);
            return View(await projectContext.ToListAsync());
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            //var cart = _context.Cart
            //.Include(c => c.User).Where(m => m.ID == id);

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        //public async Task<IActionResult> DetailsCUser(string? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.UserLogins.FirstOrDefaultAsync(m => m.UserId == id);


        //    var cart = await _context.Cart
        //                    .Include(c => c.User)
        //                    .FirstOrDefaultAsync(m => m.ID == id);

        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(cart);
        //}

        #region AddProduct

        // GET: Carts/Edit/5
        [HttpPost]
        //Route[("Carts/AddProduct/{Cid}/{Pid}")]
        public async Task<IActionResult> AddProduct(int? id, int? ItemId)
        {
            if (id == null)
            {
                return View("Identity/Account/Login");
            }

            var cart = await _context.Cart.FindAsync(id);
            var prod = await _context.Product.FindAsync(ItemId);

            cart.Products.Add(prod);
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "ID", "Email", cart.UserID);
            return View(cart);
        }

        [HttpGet]
        //Route[("Carts/AddProduct/{Cid}/{Pid}")]
        public async Task<IActionResult> AddProductToCart(int? id)
        {
            //Session[("cart")];
            var cart = await _context.Cart.FindAsync(id-6);
            var prod = await _context.Product.FindAsync(id);
            cart.Products = new List<Product>();
            cart.Products.Add(prod);
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "ID", "Email", cart.UserID);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        //Route[("Carts/AddProduct/{Cid}/{Pid}")]
        public async Task<IActionResult> AddToCart(int? id)
        {
            var session = HttpContext.Session.GetString("cart");
            if (session == null)
            {
                Cart cart = new Cart();
                cart.Products = new List<Product>();
                var product = _context.Product.Find(id);
                cart.Products.Add(product);
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
                //Session["cart"] = cart;

            }
            else
            {
                Cart cart = (Cart)JsonConvert.DeserializeObject<Cart>(session);
                var product = _context.Product.Find(id);
                cart.Products.Add(product);
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
            }
            return RedirectToAction("Menu", "Home");

            //return RedirectToAction("Show", "Carts");


        }

        public async Task<IActionResult> Show()

        {
            if (HttpContext.Session.GetString("cart") != null)
            {
                var cart = (Cart)JsonConvert.DeserializeObject<Cart>(HttpContext.Session.GetString("cart"));
                return View(cart);

            }
            else
            {
                Cart cart = new Cart();
                cart.Products = new List<Product>();
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
                return View(cart);

            }

        }

        public async Task<IActionResult> Payment()

        {
            var cart = (Cart)JsonConvert.DeserializeObject<Cart>(HttpContext.Session.GetString("cart"));
            return View(cart);

        }

        [HttpGet]
        //Route[("Carts/AddProduct/{Cid}/{Pid}")]
        public async Task<IActionResult> RemoveFromCart(int? id)
        {
            var session = HttpContext.Session.GetString("cart");
            Cart cart = (Cart)JsonConvert.DeserializeObject<Cart>(session);
            var product = _context.Product.Find(id);
            cart.Products.Remove(product);
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
            return RedirectToAction("Show", "Carts");
        }

            #endregion





            // GET: Carts/Create
            public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "ID", "Email");

            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TotalPrice,Status,UserID")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                cart.Products = new List<Product>();
                _context.Cart.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "ID", "Email", cart.UserID);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "ID", "Email", cart.UserID);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TotalPrice,Status,UserID")] Cart cart)
        {
            if (id != cart.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Cart.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.ID))
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
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "ID", "Email", cart.UserID);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.ID == id);
        }
    }
}
