using icounselvault.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace icounselvault.Controllers.SuperAdmin
{
    public class ManageClientsController : Controller
    {
        private readonly AppDbContext _context;
        public ManageClientsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ViewClients()
        {
            var clients = _context.CLIENT
                .Include(c => c.user)
                .ToList();
            return View("../../Views/SuperAdmin/ManageClients/ViewClients", clients);
        }

        public IActionResult ShowEditClient(int clientId)
        {
            TempData["selectedClient"] = _context.CLIENT
                .Where(cl => cl.CLIENT_ID == clientId)
                .FirstOrDefault();
            return View("../../Views/SuperAdmin/ManageClients/EditClient");
        }

        [HttpPost]
        public IActionResult EditClientStatus(string clientId, string status)
        {
            var foundCounselor = _context.CLIENT
                .Where(cl => cl.CLIENT_ID == int.Parse(clientId))
                .FirstOrDefault();
            foundCounselor.CLIENT_STATUS = status;
            _context.SaveChanges();
            var clients = _context.CLIENT
                .Include(c => c.user)
                .ToList();
            return View("../../Views/SuperAdmin/ManageClients/ViewClients", clients);
        }
    }
}
