using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TyreKlicker.API.Models;

namespace TyreKlicker.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult Stripe()
        {
            ViewData["Message"] = "Your payment page.";

            return View("Stripe");
        }
    }
}