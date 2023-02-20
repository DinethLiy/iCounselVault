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

        public IActionResult ShowEditInsertRequest(int insertRequestId)
        {
            TempData["selectedInsertRequest"] = _context.COUNSEL_DATA_INSERT_REQUEST
                .Where(ir => ir.COUNSEL_DATA_INSERT_REQUEST_ID == insertRequestId)
                .Include(ir => ir.clientGuidanceHistory)
                .FirstOrDefault();
            return View("../../Views/Admin/ManageInsertRequests/EditInsertRequest");
        }

        [HttpPost]
        public IActionResult EditInsertRequest(string insertRequestId, string guidanceAdvice, string status, string? remark)
        {
            var foundinsertRequest = _context.COUNSEL_DATA_INSERT_REQUEST
                .Where(ir => ir.COUNSEL_DATA_INSERT_REQUEST_ID == int.Parse(insertRequestId))
                .Include(ir => ir.clientGuidanceHistory)
                .FirstOrDefault();
            if (foundinsertRequest.clientGuidanceHistory.GUIDANCE_ADVICE != guidanceAdvice && remark == null)
            {
                ModelState.AddModelError("", "Please enter a remark if you edit the Guidance Advice!");
                TempData["selectedInsertRequest"] = _context.COUNSEL_DATA_INSERT_REQUEST
                    .Where(ir => ir.COUNSEL_DATA_INSERT_REQUEST_ID == int.Parse(insertRequestId))
                    .FirstOrDefault();
                return View("../../Views/Admin/ManageInsertRequests/EditInsertRequest");
            }
            else
            {
                foundinsertRequest.clientGuidanceHistory.GUIDANCE_ADVICE = guidanceAdvice;
                foundinsertRequest.INSERT_REQUEST_STATUS = status;
                if (remark != null)
                {
                    foundinsertRequest.INSERT_REQUEST_REMARK = remark;
                }
                _context.SaveChanges();
                var insertRequests = _context.COUNSEL_DATA_INSERT_REQUEST
                    .Include(ir => ir.client)
                    .Include(ir => ir.counselor)
                    .Include(ir => ir.clientGuidanceHistory)
                    .ToList();
                return View("../../Views/Admin/ManageInsertRequests/ViewInsertRequests", insertRequests);
            }
        }
    }
}
