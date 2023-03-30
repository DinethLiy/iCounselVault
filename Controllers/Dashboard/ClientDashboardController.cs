using icounselvault.Business.Interfaces.Client;
using icounselvault.Business.Interfaces.Dashboard;
using icounselvault.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace icounselvault.Controllers.Dashboard
{
    public class ClientDashboardController : Controller
    {
        private readonly IClientDashboardService _clientDashboardService;

        public ClientDashboardController(IClientDashboardService clientDashboardService)
        {
            _clientDashboardService = clientDashboardService;
        }

        public IActionResult Index()
        {
            SetTempDataForClientDashboard();
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
        
        public RedirectToActionResult ShowClientGuidanceHistory()
        {
            return RedirectToAction("ViewClientGuidanceHistory", "GuidanceClient");
        }

        public RedirectToActionResult ShowInsertRequests()
        {
            return RedirectToAction("ViewInsertRequests", "ClientManageInsertRequests");
        }

        private void SetTempDataForClientDashboard() 
        {
            string accessToken = Request.Cookies["access_token"];
            ArrayList resultList = _clientDashboardService.GetClientDashboardData(accessToken);
            TempData["clientInsertRequests"] = (string)resultList[0];
            TempData["pendingInsertRequests"] = (string)resultList[1];
            TempData["guidanceRecords"] = (string)resultList[2];
            TempData["counselRequests"] = (string)resultList[3];
            TempData["name"] = (string)resultList[4];
        }
    }
}
