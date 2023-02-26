using icounselvault.Models.Profiles;
using icounselvault.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace icounselvault.Controllers.Counselor
{
    public class CounselorExperienceController : Controller
    {
        private readonly AppDbContext _context;
        private Models.Profiles.Counselor? foundCounselor;
        private CounselorExperience? counselorExperience;
        public CounselorExperienceController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ShowCounselorExperience()
        {
            SetTempDataForCounselorExperience();
            if (counselorExperience != null)
            {
                return View("../../Views/Counselor/CounselorExperience/ViewCounselorExperience");
            }
            else
            {
                return View("../../Views/Counselor/CounselorExperience/AddEditCounselorExperience");
            }
        }

        [HttpPost]
        public IActionResult AddCounselorExperience(string School, string HigherEducation, string Career)
        {
            setCounselor();
            //Create new Counselor Experience
            CounselorExperience newCounselorExp = new()
            {
                counselor = foundCounselor,
                SCHOOL_EXPERIENCE = School,
                HIGHER_EDU_EXPERIENCE = HigherEducation,
                JOB_EXPERIENCE = Career
            };
            _context.COUNSELOR_EXPERIENCE.Add(newCounselorExp);
            _context.SaveChanges();
            SetTempDataForCounselorExperience();
            return View("../../Views/Counselor/CounselorExperience/ViewCounselorExperience");

        }



        public IActionResult ShowEditCounselorExperience()
        {
            SetTempDataForCounselorExperience();
            return View("../../Views/Counselor/CounselorExperience/AddEditCounselorExperience");
        }

        [HttpPost]
        public IActionResult EditCounselorExperience(string School, string HigherEducation, string Career)
        {
            SetTempDataForCounselorExperience();
            counselorExperience.SCHOOL_EXPERIENCE = School;
            counselorExperience.HIGHER_EDU_EXPERIENCE = HigherEducation;
            counselorExperience.JOB_EXPERIENCE = Career;
            _context.SaveChanges();
            SetTempDataForCounselorExperience();
            return View("../../Views/Counselor/CounselorExperience/ViewCounselorExperience");
        }

        public void setCounselor()
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

        public void SetTempDataForCounselorExperience()
        {
            setCounselor();
            counselorExperience = _context.COUNSELOR_EXPERIENCE
                .Include(coe => coe.counselor)
                .Where(coe => coe.counselor.COUNSELOR_ID == foundCounselor.COUNSELOR_ID)
                .FirstOrDefault();
            TempData["counselorExperience"] = counselorExperience;
        }
    }
}
