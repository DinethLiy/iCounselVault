using icounselvault.Models.Auth;
using icounselvault.Models.Profiles;
using icounselvault.Utility;
using icounselvault.Utility.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace icounselvault.Controllers.Auth
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IJwtAuth _jwtAuth;
        public AuthController(AppDbContext context, IJwtAuth jwtAuth)
        {
            _context = context;
            _jwtAuth = jwtAuth;
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            EncryptDecryptText encryptDecryptText = new EncryptDecryptText();
            string encryptedPassword = "";
            if (password != null)
            {
                encryptedPassword = encryptDecryptText.EncryptText(password);
            }
            User foundAccount = _context.USER
                .Where(u => u.USERNAME == username && u.PASSWORD == encryptedPassword && u.USER_STATUS == "ACT")
                .FirstOrDefault();

            if (foundAccount != null) 
            {
                var token = _jwtAuth.Authentication(foundAccount);
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                string role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value.ToString();

                if (role == "SUPER_ADMIN")
                {
                    return View("../../Views/Dashboard/SuperAdminDashboard");
                }
                else
                {
                    return View("../../Views/Dashboard/ClientDashboard");
                }
            }            
            else 
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View("Login");
            }
        }
    }
}
