using icounselvault.Models.Counseling;
using icounselvault.Utility;
using icounselvault.Utility.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace icounselvault.Controllers.Client
{
    [Authorization(RequiredPrivilegeType = "CLIENT")]
    public class GuidanceClientController : Controller
    {
        private readonly AppDbContext _context;
        private Models.Profiles.Client? foundClient;
        public GuidanceClientController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ViewClientGuidanceHistory()
        {
            return View("../../Views/Client/GuidanceClient/ViewClientGuidanceHistory", SetGuidanceHistoryList());
        }

        public IActionResult ShowClientNewGuidanceHistory()
        {
            SetTempDataForClientNewGuidanceHistory();
            return View("../../Views/Client/GuidanceClient/ClientNewGuidanceRecord");
        }

        public IActionResult ShowGuidanceRecordInfo(int cliGuidHistID) 
        {
            SetTempDataForGuidanceRecordInfo(cliGuidHistID);
            return View("../../Views/Client/GuidanceClient/ClientGuidanceRecordInfo");
        }

        [HttpPost]
        public IActionResult ClientAddGuidanceRecord(string source, string advice, string satifaction, string createdDate) 
        {
            DateTime createdDateFormatted = DateTime.Parse(createdDate);
            SetClient();
            ClientGuidanceHistory newRecord = new()
            {
                client = foundClient,
                EXTERNAL_GUIDANCE_SOURCE = source,
                GUIDANCE_ADVICE = advice,
                CLIENT_SATISFACTION = satifaction,
                CREATED_DATE = createdDateFormatted,
                HISTORY_STATUS = "INA"
            };
            _context.CLIENT_GUIDANCE_HISTORY.Add(newRecord);
            _context.SaveChanges();
            newRecord = _context.CLIENT_GUIDANCE_HISTORY
                .Include(cgh => cgh.client)
                .OrderByDescending(cgh => cgh.CLIENT_GUIDANCE_HISTORY_ID)
                .FirstOrDefault();
            CounselDataInsertRequest newRequest = new()
            {
                clientGuidanceHistory = newRecord,
                client = foundClient,
                CREATED_DATE = DateTime.Now
            };
            _context.COUNSEL_DATA_INSERT_REQUEST.Add(newRequest);
            _context.SaveChanges();

            return View("../../Views/Client/GuidanceClient/ViewClientGuidanceHistory", SetGuidanceHistoryList());
        }

        public void SetClient()
        {
            var token = Request.Cookies["access_token"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            string? userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value.ToString();
            foundClient = _context.CLIENT
                .Include(cl => cl.user)
                .Where(cl => cl.user.USER_ID == int.Parse(userId))
                .FirstOrDefault();
        }

        public List<ClientGuidanceHistory> SetGuidanceHistoryList()
        {
            SetClient();
            return _context.CLIENT_GUIDANCE_HISTORY
                .Include(cgh => cgh.client)
                .Include(cgh => cgh.counselor)
                .Where(cgh => cgh.client == foundClient && cgh.HISTORY_STATUS != "INA")
                .ToList();
        }

        public void SetTempDataForClientNewGuidanceHistory() 
        {
            TempData["sourceList"] = new List<String>() 
            {
                "Teacher", "Parent", "Friend", "Relative",
                "Expert System", "Career Guidance Practitioner"
            };
            TempData["satisfactionList"] = new List<String>()
            {
                "Very satisfied", "Satisfied but had doubts", "Doubtful",
                "A few good points but mostly disagreed", "Strongly disagreed"
            };
        }

        public void SetTempDataForGuidanceRecordInfo(int cliGuidHistID) 
        {
            TempData["guidanceRecord"] = _context.CLIENT_GUIDANCE_HISTORY
                .Include(cgh => cgh.counselor)
                .Where(cgh => cgh.CLIENT_GUIDANCE_HISTORY_ID == cliGuidHistID)
                .FirstOrDefault();
        }
    }
}
