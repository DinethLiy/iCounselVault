using icounselvault.Models.Auth;
using icounselvault.Models.Profiles;
using icounselvault.Utility;
using icounselvault.Utility.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace icounselvault.Controllers.Counselor
{
    [Authorization(RequiredPrivilegeType = "COUNSELOR")]
    public class CounselorProfileController : Controller
    {
        private readonly AppDbContext _context;
        private Models.Profiles.Counselor? counselor;
        public CounselorProfileController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddCounselor(string Name, string DateOfBirth, string Gender, string Address, string Country, string Contact, string Email)
        {
            var token = Request.Cookies["access_token"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            string? userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value.ToString();
            var foundUser = _context.USER
                .Where(u => u.USER_ID == int.Parse(userId))
                .FirstOrDefault();
            //Create new Counselor
            Models.Profiles.Counselor newCounselor = new()
            {
                user = foundUser,
                NAME = Name,
                DOB = DateTime.Parse(DateOfBirth),
                GENDER = Gender,
                ADDRESS = Address,
                COUNTRY = Country,
                CONTACT_NUM = Contact,
                EMAIL = Email,
                COUNSELOR_STATUS = "ACT"
            };
            _context.COUNSELOR.Add(newCounselor);
            _context.SaveChanges();
            SetTempDataForCounselorProfile();
            return View("../../Views/Counselor/CounselorProfile/ViewCounselorProfile");

        }

        public IActionResult ShowCounselorProfile()
        {
            SetTempDataForCounselorProfile();
            return View("../../Views/Counselor/CounselorProfile/ViewCounselorProfile");
        }

        public IActionResult Index()
        {
            SetTempDataForCounselorProfile();
            return View("../../Views/Counselor/CounselorProfile/EditCounselorProfile");
        }

        [HttpPost]
        public IActionResult EditCounselor(string Name, string Gender, string Address, string Country, string Contact, string Email)
        {
            SetTempDataForCounselorProfile();
            counselor.NAME = Name;
            counselor.GENDER = Gender;
            counselor.ADDRESS = Address;
            counselor.COUNTRY = Country;
            counselor.CONTACT_NUM = Contact;
            counselor.EMAIL = Email;
            _context.SaveChanges();
            SetTempDataForCounselorProfile();
            return View("../../Views/Counselor/CounselorProfile/ViewCounselorProfile");
        }

        public void SetTempDataForCounselorProfile()
        {
            var token = Request.Cookies["access_token"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            string? userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value.ToString();
            counselor = _context.COUNSELOR
                .Include(co => co.user)
                .Where(co => co.user.USER_ID == int.Parse(userId))
                .FirstOrDefault();
            TempData["loggedInCounselor"] = counselor;
        }

        public RedirectToActionResult ShowCounselorExperience()
        {
            return RedirectToAction("ShowCounselorExperience", "CounselorExperience");
        }
    }
}
