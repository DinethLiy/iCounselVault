using icounselvault.Models.Counseling;
using icounselvault.Models.Profiles;
using icounselvault.Utility;
using icounselvault.Utility.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace icounselvault.Controllers.Client
{
    [Authorization(RequiredPrivilegeType = "CLIENT")]
    public class ClientManageInsertRequestsController : Controller
    {
        private readonly AppDbContext _context;
        private Models.Profiles.Client? currentClient;
        public ClientManageInsertRequestsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ViewInsertRequests()
        {
            return View("../../Views/Client/ClientManageInsertRequests/ClientViewInsertRequests", SetInsertRequestList());
        }

        public IActionResult ShowEditInsertRequest(int insertRequestId)
        {
            TempData["selectedInsertRequest"] = _context.COUNSEL_DATA_INSERT_REQUEST
                .Where(ir => ir.COUNSEL_DATA_INSERT_REQUEST_ID == insertRequestId)
                .Include(ir => ir.clientGuidanceHistory)
                .FirstOrDefault();
            return View("../../Views/Client/ClientManageInsertRequests/ClientEditInsertRequest");
        }

        [HttpPost]
        public IActionResult EditInsertRequest(string insertRequestId, string status, string? remark)
        {
            var foundinsertRequest = _context.COUNSEL_DATA_INSERT_REQUEST
                .Where(ir => ir.COUNSEL_DATA_INSERT_REQUEST_ID == int.Parse(insertRequestId))
                .Include(ir => ir.clientGuidanceHistory)
                .FirstOrDefault();
            var foundRecord = foundinsertRequest.clientGuidanceHistory;

            foundinsertRequest.INSERT_REQUEST_STATUS = status;
            if (remark != null)
            {
                foundinsertRequest.INSERT_REQUEST_REMARK = remark;
            }
            if (status == "ACCEPTED")
            {
                foundRecord.HISTORY_STATUS = "ACT";
            }
            _context.SaveChanges();

            return View("../../Views/Client/ClientManageInsertRequests/ClientViewInsertRequests", SetInsertRequestList());
        }

        public void SetClient()
        {
            var token = Request.Cookies["access_token"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            string? userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value.ToString();
            currentClient = _context.CLIENT
                .Include(cl => cl.user)
                .Where(cl => cl.user.USER_ID == int.Parse(userId))
                .FirstOrDefault();
        }

        public List<CounselDataInsertRequest> SetInsertRequestList() 
        {
            SetClient();
            return _context.COUNSEL_DATA_INSERT_REQUEST
                .Include(ir => ir.client)
                .Include(ir => ir.counselor)
                .Include(ir => ir.clientGuidanceHistory)
                .Where(ir => ir.client == currentClient && ir.counselor != null && ir.INSERT_REQUEST_STATUS == "PENDING")
                .ToList();
        }
    }
}
