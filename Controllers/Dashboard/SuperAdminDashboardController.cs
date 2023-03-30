using icounselvault.Utility.Auth;
using icounselvault.Utility;
using Microsoft.AspNetCore.Mvc;
using icounselvault.Business.Interfaces.Dashboard;
using System.Collections;

namespace icounselvault.Controllers.Dashboard
{
    public class SuperAdminDashboardController : Controller
    {
        private readonly ISuperAdminDashboardService _superAdminDashboardService;

        public SuperAdminDashboardController(ISuperAdminDashboardService superAdminDashboardService)
        {
            _superAdminDashboardService = superAdminDashboardService;
        }

        public IActionResult Index()
        {
            SetTempDataForSuperAdminDashboard();
            return View("../../Views/Dashboard/SuperAdminDashboard");
        }

        public RedirectToActionResult ShowAdmins() 
        {
            return RedirectToAction("ViewAdmins", "ManageAdmins");
        }

        public RedirectToActionResult ShowCounselors()
        {
            return RedirectToAction("SuperAdminViewCounselors", "ManageCounselors");
        }

        public RedirectToActionResult ShowClients()
        {
            return RedirectToAction("SuperAdminViewClients", "ManageClients");
        }

        private void SetTempDataForSuperAdminDashboard() 
        {
            ArrayList resultList = _superAdminDashboardService.GetDataForSuperAdminDashboard();
            TempData["superCount"] = (string)resultList[0];
            TempData["adminCount"] = (string)resultList[1];
            TempData["clientCount"] = (string)resultList[2];
            TempData["counselorCount"] = (string)resultList[3];
        }
    }
}
