using icounselvault.Models.Profiles;
using icounselvault.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace icounselvault.Controllers.Client
{
    public class ClientExperienceController : Controller
    {
        private readonly AppDbContext _context;
        private Models.Profiles.Client? foundClient;
        private ClientExperience? clientExperience;
        public ClientExperienceController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ShowClientExperience()
        {
            SetTempDataForClientExperience();
            if (clientExperience != null)
            {
                return View("../../Views/Client/ClientExperience/ViewClientExperience");
            }
            else 
            {
                return View("../../Views/Client/ClientExperience/AddEditClientExperience");
            }
        }

        [HttpPost]
        public IActionResult AddClientExperience(string School, string HigherEducation, string Career, string PreferredCareer)
        {
            SetClient();
            //Create new Client Experience
            ClientExperience newClientExp = new()
            {
                client = foundClient,
                SCHOOL_EXPERIENCE = School,
                HIGHER_EDU_EXPERIENCE = HigherEducation,
                JOB_EXPERIENCE = Career,
                PREFERED_CAREER_FIELD = PreferredCareer,
            };
            _context.CLIENT_EXPERIENCE.Add(newClientExp);
            _context.SaveChanges();
            SetTempDataForClientExperience();
            return View("../../Views/Client/ClientExperience/ViewClientExperience");

        }

        

        public IActionResult ShowEditClientExperience()
        {
            SetTempDataForClientExperience();
            return View("../../Views/Client/ClientExperience/AddEditClientExperience");
        }

        [HttpPost]
        public IActionResult EditClientExperience(string School, string HigherEducation, string Career, string PreferredCareer)
        {
            SetTempDataForClientExperience();
            clientExperience.SCHOOL_EXPERIENCE = School;
            clientExperience.HIGHER_EDU_EXPERIENCE = HigherEducation;
            clientExperience.JOB_EXPERIENCE = Career;
            clientExperience.PREFERED_CAREER_FIELD = PreferredCareer;
            _context.SaveChanges();
            SetTempDataForClientExperience();
            return View("../../Views/Client/ClientExperience/ViewClientExperience");
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

        public void SetTempDataForClientExperience()
        {
            SetClient();
            clientExperience = _context.CLIENT_EXPERIENCE
                .Include(cle => cle.client)
                .Where(cle => cle.client.CLIENT_ID == foundClient.CLIENT_ID)
                .FirstOrDefault();
            TempData["clientExperience"] = clientExperience;
        }
    }
}
