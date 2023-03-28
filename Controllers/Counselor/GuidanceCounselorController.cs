using icounselvault.Models.Counseling;
using icounselvault.Models.Profiles;
using icounselvault.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace icounselvault.Controllers.Counselor
{
    public class GuidanceCounselorController : Controller
    {
        private readonly AppDbContext _context;
        private Models.Profiles.Counselor? foundCounselor;
        public GuidanceCounselorController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ViewCounselorGuidanceHistory()
        {
            return View("../../Views/Counselor/GuidanceCounselor/ViewCounselorGuidanceHistory", SetGuidanceHistoryList());
        }

        public IActionResult ShowGuidanceRecordInfo(int cliGuidHistID)
        {
            SetTempDataForGuidanceRecordInfo(cliGuidHistID);
            return View("../../Views/Counselor/GuidanceCounselor/CounselorGuidanceRecordInfo");
        }

        public IActionResult ShowCounselorNewGuidanceHistory()
        {
            return View("../../Views/Counselor/GuidanceCounselor/CounselorNewGuidanceRecord");
        }

        [HttpPost]
        public IActionResult FindClient(string code) 
        {
            var foundClient = _context.CLIENT
                .Where(cl => cl.CLIENT_CODE.ToString() == code)
                .FirstOrDefault();
            List<ClientGuidanceHistory> foundGuidance = new();
            foundGuidance = _context.CLIENT_GUIDANCE_HISTORY
                .Include(cgh => cgh.client)
                .Where(cgh => cgh.client == foundClient && cgh.HISTORY_STATUS != "INA")
                .ToList();
            var foundExperience = _context.CLIENT_EXPERIENCE
                .Include(cle => cle.client)
                .Where(cle => cle.client == foundClient)
                .FirstOrDefault();
            if (foundClient != null)
            {
                SetTempDataForCounselorNewGuidanceHistory(foundClient, foundGuidance, foundExperience);
                return View("../../Views/Counselor/GuidanceCounselor/CounselorNewGuidanceRecord");
            }
            else 
            {
                ModelState.AddModelError("", "Invalid Client Code!");
                return View("../../Views/Counselor/GuidanceCounselor/CounselorNewGuidanceRecord");
            }
        }

        [HttpPost]
        public IActionResult CounselorAddGuidanceRecord(string clientID, string advice, string satifaction, string createdDate)
        {
            DateTime createdDateFormatted = DateTime.Parse(createdDate);
            SetCounselor();
            var foundClient = _context.CLIENT
                .Where(cl => cl.CLIENT_ID.ToString() == clientID)
                .FirstOrDefault();

            ClientGuidanceHistory newRecord = new()
            {
                client = foundClient,
                counselor = foundCounselor,
                GUIDANCE_ADVICE = advice,
                CLIENT_SATISFACTION = satifaction,
                CREATED_DATE = createdDateFormatted,
                HISTORY_STATUS = "INA"
            };
            _context.CLIENT_GUIDANCE_HISTORY.Add(newRecord);
            _context.SaveChanges();
            newRecord = _context.CLIENT_GUIDANCE_HISTORY
                .Include(cgh => cgh.client)
                .Include(cgh => cgh.counselor)
                .OrderByDescending(cgh => cgh.CLIENT_GUIDANCE_HISTORY_ID)
                .FirstOrDefault();
            CounselDataInsertRequest newRequest = new()
            {
                clientGuidanceHistory = newRecord,
                client = foundClient,
                counselor = foundCounselor,
                CREATED_DATE = DateTime.Now
            };
            _context.COUNSEL_DATA_INSERT_REQUEST.Add(newRequest);
            _context.SaveChanges();

            return View("../../Views/Counselor/GuidanceCounselor/ViewCounselorGuidanceHistory", SetGuidanceHistoryList());
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

        public List<ClientGuidanceHistory> SetGuidanceHistoryList()
        {
            SetCounselor();
            return _context.CLIENT_GUIDANCE_HISTORY
                .Include(cgh => cgh.client)
                .Include(cgh => cgh.counselor)
                .Where(cgh => cgh.counselor == foundCounselor && cgh.HISTORY_STATUS != "INA")
                .ToList();
        }

        public void SetTempDataForGuidanceRecordInfo(int cliGuidHistID)
        {
            TempData["guidanceRecord"] = _context.CLIENT_GUIDANCE_HISTORY
                .Include(cgh => cgh.counselor)
                .Include(cgh => cgh.client)
                .Where(cgh => cgh.CLIENT_GUIDANCE_HISTORY_ID == cliGuidHistID)
                .FirstOrDefault();
        }

        public void SetTempDataForCounselorNewGuidanceHistory(Models.Profiles.Client foundClient, List<ClientGuidanceHistory> foundGuidance, ClientExperience? foundExperience)
        {
            TempData["foundClient"] = foundClient;
            TempData["foundClientGuidance"] = foundGuidance;
            TempData["foundClientExperience"] = foundExperience;
            TempData["satisfactionList"] = new List<String>()
            {
                "Very satisfied", "Satisfied but had doubts", "Doubtful",
                "A few good points but mostly disagreed", "Strongly disagreed"
            };
        }
    }
}
