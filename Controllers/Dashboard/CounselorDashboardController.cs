using icounselvault.Business.Interfaces.Dashboard;
using icounselvault.Business.Services.Dashboard;
using icounselvault.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace icounselvault.Controllers.Dashboard
{
    public class CounselorDashboardController : Controller
    {
        private readonly ICounselorDashboardService _counselorDashboardService;

        public CounselorDashboardController(ICounselorDashboardService counselorDashboardService)
        {
            _counselorDashboardService = counselorDashboardService;
        }

        public IActionResult Index()
        {
            SetTempDataForCounselorDashboard();
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

        public RedirectToActionResult ShowCounselorGuidanceHistory()
        {
            return RedirectToAction("ViewCounselorGuidanceHistory", "GuidanceCounselor");
        }

        private void SetTempDataForCounselorDashboard()
        {
            string accessToken = Request.Cookies["access_token"];
            ArrayList resultList = _counselorDashboardService.GetCounselorDashboardData(accessToken);
            TempData["selfCounselRequests"] = (string)resultList[0];
            TempData["pendingInsertRequests"] = (string)resultList[1];
            TempData["guidanceRecords"] = (string)resultList[2];
            TempData["counselRequests"] = (string)resultList[3];
            TempData["name"] = (string)resultList[4];
        }
    }
}
