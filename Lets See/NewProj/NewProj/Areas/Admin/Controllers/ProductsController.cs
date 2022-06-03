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
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using NewProj.Models.Interfaces;

namespace NewProj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ProductsController : Controller
    {
        private IProductRepository Product_Repostory;
        private ICategoryRepository Catagory_Repostory;
        private IOrderRepository Order_Repostory;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(IProductRepository r, ICategoryRepository r2,
            IOrderRepository r3,IWebHostEnvironment hostEnvironment)
        {
            Product_Repostory = r;
            Catagory_Repostory=r2;
            Order_Repostory=r3;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Admin/Products
        
        public async Task<IActionResult> Index()
        {
            var projectContext = Product_Repostory.GetProducts().Include(p => p.CategoryNavigation).Include(p => p.Order);
            return View(await projectContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await Product_Repostory.GetProducts()
                .Include(p => p.CategoryNavigation)
                .Include(p => p.Order)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(Catagory_Repostory.GetCategories(), "ID", "Name");
            ViewData["OrderID"] = new SelectList(Order_Repostory.GetOrders(), "ID", "ID");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("ID,Name,Description,Price,Category,Photo,CategoryID,OrderID")] Product product)
        {
            if (ModelState.IsValid)
            {
               
                if (product.Photo != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(product.Photo.FileName);
                    string extension = Path.GetExtension(product.Photo.FileName);
                    product.PhotoSrc = fileName = fileName + extension;
                    string path = Path.Combine(wwwRootPath + "/template/Images", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await product.Photo.CopyToAsync(fileStream);
                    }
                }

                Product_Repostory.Add(product);
                await Product_Repostory.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
        
            ViewData["CategoryID"] = new SelectList(Catagory_Repostory.GetCategories(), "ID", "Name", product.CategoryID);
            ViewData["OrderID"] = new SelectList(Order_Repostory.GetOrders(), "ID", "ID", product.OrderID);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await Product_Repostory.GetProducts().FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(Catagory_Repostory.GetCategories(), "ID", "Name", product.CategoryID);
            ViewData["OrderID"] = new SelectList(Order_Repostory.GetOrders(), "ID", "ID", product.OrderID);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Price,Category,Photo,CategoryID,OrderID")] Product product)
        {
            if (id != product.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.Photo != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(product.Photo.FileName);
                        string extension = Path.GetExtension(product.Photo.FileName);

                        //product.PhotoSrc = product.Photo.FileName;
                        product.PhotoSrc = fileName = fileName + extension;


                        string path = Path.Combine(wwwRootPath + "/template/images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await product.Photo.CopyToAsync(fileStream);
                        }
                    }
                    Product_Repostory.Update(product);
                    await Product_Repostory.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
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
            ViewData["CategoryID"] = new SelectList(Catagory_Repostory.GetCategories(), "ID", "Name", product.CategoryID);
            ViewData["OrderID"] = new SelectList(Order_Repostory.GetOrders(), "ID", "ID", product.OrderID);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await Product_Repostory.GetProducts()
                .Include(p => p.CategoryNavigation)
                .Include(p => p.Order)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await Product_Repostory.GetProducts().FindAsync(id);
            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "/template/images/", product.PhotoSrc);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            Product_Repostory.Delete(product);
            await Product_Repostory.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return Product_Repostory.GetProducts().Any(e => e.ID == id);
        }
    }
}
