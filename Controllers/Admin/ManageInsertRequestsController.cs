using icounselvault.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace icounselvault.Controllers.Admin
{
    public class ManageInsertRequestsController : Controller
    {
        private readonly AppDbContext _context;
        public ManageInsertRequestsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ViewInsertRequests()
        {
            var insertRequests = _context.COUNSEL_DATA_INSERT_REQUEST
                .Include(ir => ir.client)
                .Include(ir => ir.counselor)
                .Include(ir => ir.clientGuidanceHistory)
                .ToList();
            return View("../../Views/Admin/ManageInsertRequests/ViewInsertRequests", insertRequests);
        }
    }
}
