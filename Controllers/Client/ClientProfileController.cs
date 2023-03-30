using icounselvault.Models.Auth;
using icounselvault.Models.Profiles;
using icounselvault.Utility;
using icounselvault.Utility.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Authorization = icounselvault.Utility.Auth.Authorization;

namespace icounselvault.Controllers.Client
{
    [Authorization(RequiredPrivilegeType = "CLIENT")]
    public class ClientProfileController : Controller
    {
        private readonly AppDbContext _context;
        private Models.Profiles.Client? client;
        public ClientProfileController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ShowClientProfile()
        {
            SetTempDataForClientProfile();
            return View("../../Views/Client/ClientProfile/ViewClientProfile");
        }

        public IActionResult ShowEditClientProfile() 
        {
            SetTempDataForClientProfile();
            return View("../../Views/Client/ClientProfile/EditClientProfile");
        }

        [HttpPost]
        public IActionResult EditClient(string Name, string Gender, string Address, string Country, string Contact, string Email) 
        {
            SetTempDataForClientProfile();
            client.NAME = Name;
            client.GENDER = Gender;
            client.ADDRESS = Address;
            client.COUNTRY = Country;
            client.CONTACT_NUM = Contact;
            client.EMAIL = Email;
            _context.SaveChanges();
            SetTempDataForClientProfile();
            return View("../../Views/Client/ClientProfile/ViewClientProfile");
        }

        public void SetTempDataForClientProfile()
        {
            var token = Request.Cookies["access_token"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            string? userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value.ToString();
            client = _context.CLIENT
                .Include(cl => cl.user)
                .Where(cl => cl.user.USER_ID == int.Parse(userId))
                .FirstOrDefault();
            TempData["loggedInClient"] = client;
        }

        public RedirectToActionResult ShowClientExperience() 
        {
            return RedirectToAction("ShowClientExperience", "ClientExperience");
        }
    }
}
