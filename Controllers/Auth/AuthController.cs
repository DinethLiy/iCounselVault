using icounselvault.Models.Auth;
using icounselvault.Models.Profiles;
using icounselvault.Utility;
using icounselvault.Utility.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult IncorrectLogin() 
        {
            ModelState.AddModelError("", "Invalid username or password");
            return View("Login");
        }

        public IActionResult AccessDenied()
        {
            ModelState.AddModelError("", "Access Denied");
            // Cookie containing JWT Auth key is destroyed at logout.
            Response.Cookies.Delete("access_token");
            return View("Login");
        }

        public IActionResult Logout()
        {
            ModelState.AddModelError("", "Logged out Successfully!");
            // Cookie containing JWT Auth key is destroyed at logout.
            Response.Cookies.Delete("access_token");
            return View("Login");
        }

        [HttpPost]
        public RedirectToActionResult Login(string username, string password)
        {
            EncryptDecryptText encryptDecryptText = new();
            string encryptedPassword = "";
            if (password != null)
            {
                encryptedPassword = encryptDecryptText.EncryptText(password);
            }
            User? foundAccount = _context.USER
                .Where(u => u.USERNAME == username && u.PASSWORD == encryptedPassword && u.USER_STATUS == "ACT")
                .FirstOrDefault();

            if (foundAccount != null) 
            {
                string? role = LoggedInUserAuthentication(foundAccount);
                return RedirectToAction("Index", RedirectToDashboard(foundAccount, role));
            }            
            else 
            {
                return RedirectToAction("IncorrectLogin", "Auth");
            }
        }

        public string? LoggedInUserAuthentication(User foundAccount) 
        {
            var token = _jwtAuth.Authentication(foundAccount);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            string? role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value.ToString();
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // only transmit over HTTPS
                SameSite = SameSiteMode.Strict, // prevent CSRF attacks
                Expires = DateTime.UtcNow.AddHours(1)
            };
            Response.Cookies.Append("access_token", token, cookieOptions);
            return role;
        }

        public string RedirectToDashboard(User foundAccount, string? role)
        {
            if (role == "SUPER_ADMIN")
            {
                return "SuperAdminDashboard";
            }
            else if (role == "ADMIN")
            {
                return "AdminDashboard";
            }
            else if (role == "COUNSELOR")
            {
                return RedirectCounselor(foundAccount);
            }
            else
            {
                return "ClientDashboard";
            }
        }

        public string RedirectCounselor(User foundAccount) 
        {
            var foundCounselor = _context.COUNSELOR
                        .Include(c => c.user)
                        .Where(c => c.user.USER_ID == foundAccount.USER_ID)
                        .FirstOrDefault();
            if (foundCounselor != null)
            {
                return "CounselorDashboard";
            }
            else
            {
                return "CounselorProfile";
            }
        }

        public IActionResult ShowClientRegister() 
        {
            return View("../../Views/Auth/ClientRegister");
        }

        [HttpPost]
        public IActionResult AddClient(string Username, string Password, string ConfirmPassword, string Name, string DateOfBirth, string Gender, string Address, string Country, string Contact, string Email) 
        {
            EncryptDecryptText encryptDecryptText = new();
            if (Username != null && Password != null && ConfirmPassword == Password)
            {
                //Create new client-user
                User newClientUser = new()
                {
                    USERNAME = Username,
                    PASSWORD = encryptDecryptText.EncryptText(Password),
                    PRIVILEGE_TYPE = "CLIENT",
                    USER_STATUS = "ACT"
                };
                _context.USER.Add(newClientUser);
                _context.SaveChanges();
                //Get created client-user
                var createdClient = _context.USER
                    .OrderByDescending(u => u.USER_ID)
                    .FirstOrDefault();
                //Create new Client
                icounselvault.Models.Profiles.Client newClient = new()
                {
                    user = createdClient,
                    CLIENT_CODE = Guid.NewGuid(),
                    NAME = Name,
                    DOB = DateTime.Parse(DateOfBirth),
                    GENDER = Gender,
                    ADDRESS = Address,
                    COUNTRY = Country,
                    CONTACT_NUM = Contact,
                    EMAIL= Email,
                    CLIENT_STATUS = "ACT"
                };
                _context.CLIENT.Add(newClient);
                _context.SaveChanges();
                ModelState.AddModelError("", "Succesfully created a new Account! Please Log in");
                return View("../../Views/Auth/Login");
            }
            else
            {
                ModelState.AddModelError("", "Error, Please enter valid values into the fields.");
                return View("../../Views/Auth/ClientRegister");
            }
        }
    }
}
