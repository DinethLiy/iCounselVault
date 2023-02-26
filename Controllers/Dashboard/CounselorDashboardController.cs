using icounselvault.Utility;
using Microsoft.AspNetCore.Mvc;

namespace icounselvault.Controllers.Dashboard
{
    public class CounselorDashboardController : Controller
    {
        private readonly AppDbContext _context;
        public CounselorDashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("../../Views/Dashboard/CounselorDashboard");
        }

        public RedirectToActionResult ShowCounselorProfile()
        {
            return RedirectToAction("ShowCounselorProfile", "CounselorProfile");
        }
        
        public RedirectToActionResult ShowCounselRequests()
        {
            return RedirectToAction("ViewCounselorCounselRequests", "CounselorCounselRequests");
        }
    }
}
