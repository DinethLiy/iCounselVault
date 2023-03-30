using icounselvault.Utility;
using icounselvault.Utility.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace icounselvault.Controllers.Admin
{
    [Authorization(RequiredPrivilegeType = "ADMIN")]
    public class ManageCounselRequestsController : Controller
    {
        private readonly AppDbContext _context;
        public ManageCounselRequestsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ViewCounselRequests()
        {
            var counselRequests = _context.COUNSEL_REQUEST
                .Include(cr => cr.client)
                .Include(cr => cr.counselor)
                .ToList();
            return View("../../Views/Admin/ViewCounselRequests/ViewCounselRequests", counselRequests);
        }
    }
}
