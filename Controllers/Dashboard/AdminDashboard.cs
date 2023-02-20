﻿using icounselvault.Utility;
using Microsoft.AspNetCore.Mvc;

namespace icounselvault.Controllers.Dashboard
{
    public class AdminDashboard : Controller
    {
        private readonly AppDbContext _context;
        public AdminDashboard(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("../../Views/Dashboard/SuperAdminDashboard");
        }

        public RedirectToActionResult ShowCounselors()
        {
            return RedirectToAction("ViewCounselors", "ManageCounselors");
        }

        public RedirectToActionResult ShowClients()
        {
            return RedirectToAction("ViewClients", "ManageClients");
        }
        
        public RedirectToActionResult ShowInsertRequests()
        {
            return RedirectToAction("ViewInsertRequests", "ManageInsertRequests");
        }
    }
}
