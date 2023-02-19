using icounselvault.Utility.Auth;
using icounselvault.Utility;
using Microsoft.AspNetCore.Mvc;

namespace icounselvault.Controllers.Dashboard
{
    public class SuperAdminDashboardController : Controller
    {
        private readonly AppDbContext _context;
        public SuperAdminDashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("../../Views/Dashboard/SuperAdminDashboard");
        }

        public RedirectToActionResult ShowAdmins() 
        {
            return RedirectToAction("ViewAdmins", "ManageAdmins");
        }

        public RedirectToActionResult ShowCounselors()
        {
            return RedirectToAction("ViewCounselors", "ManageCounselors");
        }

        public RedirectToActionResult ShowClients()
        {
            return RedirectToAction("ViewClients", "ManageClients");
        }
    }
}
