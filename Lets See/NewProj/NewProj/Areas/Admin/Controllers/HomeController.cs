using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewProj.Models;
using NewProj.Classes;
using Microsoft.AspNetCore.Authorization;

namespace NewProj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class HomeController : Controller
    {
        private readonly ProjectContext _context;
        public HomeController(ProjectContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var projectContext = _context.Orders.Include(o => o.Cart);
            return View(await projectContext.ToListAsync());
        }
    }
}
