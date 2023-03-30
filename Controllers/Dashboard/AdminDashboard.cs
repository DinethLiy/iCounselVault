using icounselvault.Business.Interfaces.Dashboard;
using icounselvault.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace icounselvault.Controllers.Dashboard
{
    public class AdminDashboard : Controller
    {
        private readonly IAdminDashboardService _adminDashboardService;

        public AdminDashboard(IAdminDashboardService adminDashboardService)
        {
            _adminDashboardService = adminDashboardService;
        }

        public IActionResult Index()
        {
            SetTempDataForAdminDashboard();
            return View("../../Views/Dashboard/AdminDashboard");
        }

        public RedirectToActionResult ShowCounselors()
        {
            return RedirectToAction("AdminViewCounselors", "ManageCounselors");
        }

        public RedirectToActionResult ShowClients()
        {
            return RedirectToAction("AdminViewClients", "ManageClients");
        }
        
        public RedirectToActionResult ShowInsertRequests()
        {
            return RedirectToAction("ViewInsertRequests", "ManageInsertRequests");
        }

        public RedirectToActionResult ShowCounselRequests()
        {
            return RedirectToAction("ViewCounselRequests", "ManageCounselRequests");
        }

        public RedirectToActionResult ShowSurveyCountReport()
        {
            return RedirectToAction("ViewSurveyCountReport", "Report");
        }

        public RedirectToActionResult ShowCounselRequestsReport()
        {
            return RedirectToAction("ViewCounselRequestsReport", "Report");
        }

        public RedirectToActionResult ShowDataInsertRequestsReport()
        {
            return RedirectToAction("ViewDataInsertRequestsReport", "Report");
        }

        public RedirectToActionResult ShowCounselorActivityReport()
        {
            return RedirectToAction("ViewCounselorActivityReport", "Report");
        }

        public RedirectToActionResult ShowClientActivityReport()
        {
            return RedirectToAction("ViewClientActivityReport", "Report");
        }

        private void SetTempDataForAdminDashboard() 
        {
            ArrayList resultList = _adminDashboardService.GetDataForAdminDashboard();
            TempData["pendingDataInsertRequests"] = (string)resultList[0];
            TempData["pendingCounselRequests"] = (string)resultList[1];
            TempData["clientCount"] = (string)resultList[2];
            TempData["counselorCount"] = (string)resultList[3];
        }
    }
}
