using icounselvault.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace icounselvault.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("LandingPage");
        }
    }
}