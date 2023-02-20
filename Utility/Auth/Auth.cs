using icounselvault.Models.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace icounselvault.Utility.Auth
{
    public class Auth : IJwtAuth
    {
        private readonly string key;
        public Auth(string key)
        {
            this.key = key;
        }

        public string Authentication(User user)
        {
            // Set Claims for Token
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.PRIVILEGE_TYPE),
                new Claim("UserId", user.USER_ID.ToString())
            };

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(key);

            // 3. Create Token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature));

            // 4. Return Token from method
            return tokenHandler.WriteToken(token);
        }
    }
}
