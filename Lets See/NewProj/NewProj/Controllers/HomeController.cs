using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewProj.Models;
using Stripe;
using System.Diagnostics;

namespace NewProj.Controllers
{
    public class HomeController : Controller
    {

        

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        //[Authorize(Roles="Admin")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Charge(string stripeEmail, string stripeToken)
        {



            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
               Source = stripeToken
            }) ;

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = 500,
                Description = "Sample Charge",
                Currency = "usd",
                Customer = customer.Id
            });

            return View();
        }


    }
}