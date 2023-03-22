using icounselvault.Business.Interfaces.Auth;
using icounselvault.Models.Auth;
using icounselvault.Utility;
using icounselvault.Utility.Auth;
using System.IdentityModel.Tokens.Jwt;

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
    }
}
