using icounselvault.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace icounselvault.Controllers.Dashboard
{
    public class ClientDashboardController : Controller
    {
        private readonly AppDbContext _context;
        public ClientDashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("../../Views/Dashboard/ClientDashboard");
        }

        public RedirectToActionResult ShowClientProfile()
        {
            return RedirectToAction("ShowClientProfile", "ClientProfile");
        }
        
        public RedirectToActionResult ShowSearchCounselors()
        {
            return RedirectToAction("ShowSearchCounselors", "ClientCounselRequests");
        }

        
    }
}
