using icounselvault.Models.Counseling;
using icounselvault.Utility;
using icounselvault.Utility.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace icounselvault.Controllers.Counselor
{
    [Authorization(RequiredPrivilegeType = "COUNSELOR")]
    public class CounselorCounselRequestsController : Controller
    {
        private readonly AppDbContext _context;
        private Models.Profiles.Counselor? foundCounselor;
        public CounselorCounselRequestsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ViewCounselorCounselRequests()
        {
            return View("../../Views/Counselor/CounselRequests/CounselorViewCounselRequests", SetCounselRequestList());
        }

        public IActionResult ShowEditCounselRequest(int counselRequestID)
        {
            SetTempDataForCreateCounselRequest(counselRequestID);
            return View("../../Views/Counselor/CounselRequests/CounselorManageCounselRequests");
        }

        public void SetCounselor()
        {
            var token = Request.Cookies["access_token"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            string? userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value.ToString();
            foundCounselor = _context.COUNSELOR
                .Include(co => co.user)
                .Where(co => co.user.USER_ID == int.Parse(userId))
                .FirstOrDefault();
        }

        public List<CounselRequest> SetCounselRequestList() 
        {
            SetCounselor();
            return _context.COUNSEL_REQUEST
                .Include(cr => cr.client)
                .Include(cr => cr.counselor)
                .Where(cr => cr.counselor == foundCounselor)
                .ToList();
        }

        public void SetTempDataForCreateCounselRequest(int counselRequestId) 
        {
            var counselRequest = _context.COUNSEL_REQUEST
                .Include(cr => cr.client)
                .Include(cr => cr.counselor)
                .Where(cr => cr.COUNSEL_REQUEST_ID == counselRequestId)
                .FirstOrDefault();
            TempData["counselRequest"] = counselRequest;
        }

        [HttpPost]
        public IActionResult EditCounselRequest(string counselRequestId, string Status, string Remark)
        {
            Remark ??= " ";
            var foundCounselRequest = _context.COUNSEL_REQUEST
                .Include(cr => cr.client)
                .Include(cr => cr.counselor)
                .Where(cr => cr.COUNSEL_REQUEST_ID == int.Parse(counselRequestId))
                .FirstOrDefault();
            foundCounselRequest.COUNSEL_REQUEST_STATUS = Status;
            foundCounselRequest.COUNSEL_REQUEST_REMARK = Remark;
            _context.SaveChanges();
            return View("../../Views/Counselor/CounselRequests/CounselorViewCounselRequests", SetCounselRequestList());
        }
    }
}
