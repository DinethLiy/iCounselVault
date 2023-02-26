using icounselvault.Models.Counseling;
using icounselvault.Models.Profiles;
using icounselvault.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace icounselvault.Controllers.Client
{
    public class ClientCounselRequestsController : Controller
    {
        private readonly AppDbContext _context;
        private Models.Profiles.Client? foundClient;
        private CounselRequest? counselRequest;
        public ClientCounselRequestsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ShowSearchCounselors() 
        {
            SetTempDataForShowCounselRequests();
            return View("../../Views/Client/FindCounselor/SearchCounselors", GetCounselorsList());
        }

        public IActionResult ShowAddCounselRequest(int counselorId) 
        {
            SetTempDataForCreateCounselRequest(counselorId);
            return View("../../Views/Client/FindCounselor/CreateCounselRequest");
        }

        [HttpPost]
        public IActionResult AddCounselRequest(string counselorId, string StartDate, string EndDate) 
        {
            if (StartDate != null && EndDate != null)
            {
                DateTime startDate = DateTime.Parse(StartDate);
                DateTime endDate = DateTime.Parse(EndDate);
                if (startDate > endDate)
                {
                    ModelState.AddModelError("", "Please enter a starting date before the ending date!");
                    SetTempDataForCreateCounselRequest(int.Parse(counselorId));
                    return View("../../Views/Client/FindCounselor/CreateCounselRequest");
                }
                else
                {
                    SetClient();
                    CounselRequest newCounselRequest = new()
                    {
                        counselor = _context.COUNSELOR.Where(co => co.COUNSELOR_ID == int.Parse(counselorId)).FirstOrDefault(),
                        client = foundClient,
                        FROM_DATE = startDate,
                        TO_DATE = endDate,
                        CREATED_DATE = DateTime.Now,
                    };
                    _context.COUNSEL_REQUEST.Add(newCounselRequest);
                    _context.SaveChanges();
                    SetTempDataForShowCounselRequests();
                    return View("../../Views/Client/FindCounselor/SearchCounselors", GetCounselorsList());
                }
            }
            else 
            {
                ModelState.AddModelError("", "Please enter valid dates for the date ranges!");
                SetTempDataForCreateCounselRequest(int.Parse(counselorId));
                return View("../../Views/Client/FindCounselor/CreateCounselRequest");
            }
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

        public void SetTempDataForShowCounselRequests() 
        {
            SetClient();
            counselRequest = _context.COUNSEL_REQUEST
                .Include(cr => cr.client)
                .Include(cr => cr.counselor)
                .Where(cr => cr.client.CLIENT_ID == foundClient.CLIENT_ID
                          && (cr.COUNSEL_REQUEST_STATUS != "REJECTED" || cr.TO_DATE < DateTime.Now))
                .FirstOrDefault();
            TempData["counselRequest"] = counselRequest;
        }

        public List<Models.Profiles.Counselor> GetCounselorsList() 
        {
            return _context.COUNSELOR
                .Where(co => co.COUNSELOR_STATUS != "INA")
                .ToList();
        }

        public void SetTempDataForCreateCounselRequest(int counselorId) 
        {
            var counselor = _context.COUNSELOR
                .Where(co => co.COUNSELOR_ID == counselorId)
                .FirstOrDefault();
            TempData["counselor"] = counselor;
        }
    }
}
