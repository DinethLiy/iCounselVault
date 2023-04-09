using icounselvault.Utility;
using icounselvault.Utility.Auth;
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

        [Authorization(RequiredPrivilegeType = "SUPER_ADMIN")]
        public IActionResult SuperAdminViewClients()
        {
            TempData["layout"] = "../../Views/Shared/_SuperAdminLayout";
            return View("../../Views/SuperAdmin/ManageClients/ViewClients", GetClientList());
        }

        [Authorization(RequiredPrivilegeType = "ADMIN")]
        public IActionResult AdminViewClients()
        {
            TempData["layout"] = "../../Views/Shared/_AdminLayout";
            return View("../../Views/SuperAdmin/ManageClients/ViewClients", GetClientList());
        }

        [Authorization(RequiredPrivilegeType = "SUPER_ADMIN")]
        public IActionResult SuperAdminShowEditClient(int clientId)
        {
            SetEditClientTempData(clientId);
            TempData["layout"] = "../../Views/Shared/_SuperAdminLayout";
            return View("../../Views/SuperAdmin/ManageClients/EditClient");
        }

        [Authorization(RequiredPrivilegeType = "ADMIN")]
        public IActionResult AdminShowEditClient(int clientId)
        {
            SetEditClientTempData(clientId);
            TempData["layout"] = "../../Views/Shared/_AdminLayout";
            return View("../../Views/SuperAdmin/ManageClients/EditClient");
        }

        [HttpPost]
        public IActionResult EditClientStatus(string clientId, string status, string layoutPath)
        {
            TempData["layout"] = layoutPath;
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

        private List<Models.Profiles.Client> GetClientList() 
        {
            return _context.CLIENT
                .Include(c => c.user)
                .ToList();
        }

        private void SetEditClientTempData(int clientId) 
        {
            TempData["selectedClient"] = _context.CLIENT
                .Where(cl => cl.CLIENT_ID == clientId)
                .FirstOrDefault();
        }
    }
}
