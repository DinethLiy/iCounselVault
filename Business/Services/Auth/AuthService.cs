using icounselvault.Business.Interfaces.Auth;
using icounselvault.Models.Auth;
using icounselvault.Utility;
using icounselvault.Utility.Auth;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace icounselvault.Business.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IJwtAuth _jwtAuth;
        private string token;
        private CookieOptions cookieOptions;
        private string privilegeType;

        public AuthService(AppDbContext context, IJwtAuth jwtAuth)
        {
            _context = context;
            _jwtAuth = jwtAuth;
        }

        // Returns the logged in user stored in the Cookie (Cookie provides the accessToken)
        public User? GetLoggedInUser(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);
            string? userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value.ToString();
            return _context.USER
                .Where(u => u.USER_ID == int.Parse(userId))
                .FirstOrDefault();
        }

        // Sends the user to their designated homepage if the username and password match
        public string LoginUser(string username, string password)
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
                privilegeType = LoggedInUserAuthentication(foundAccount);
                return RedirectToDashboard(foundAccount, privilegeType);
            }
            else
            {
                return "error";
            }
        }

        public string GetToken()
        {
            return token;
        }

        public CookieOptions GetCookieOptions()
        {
            return cookieOptions;
        }

        public string GetPrivilegeType()
        {
            return privilegeType;
        }

        private string LoggedInUserAuthentication(User foundAccount)
        {
            token = _jwtAuth.Authentication(foundAccount);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            string? role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value.ToString();
            cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // only transmit over HTTPS
                SameSite = SameSiteMode.Strict, // prevent CSRF attacks
                Expires = DateTime.UtcNow.AddHours(1)
            };
            role ??= "empty";
            return role;
        }

        private string RedirectToDashboard(User foundAccount, string? role)
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

        private string RedirectCounselor(User foundAccount)
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
    }
}
