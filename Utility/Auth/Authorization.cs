using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace icounselvault.Utility.Auth
{
    public class Authorization : ActionFilterAttribute
    {
        public string RequiredPrivilegeType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var token = filterContext.HttpContext.Request.Cookies["access_token"];

            if (string.IsNullOrEmpty(token))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                { "Controller", "Auth" },
                { "Action", "AccessDenied" }
                    });
                return;
            }

            try
            {
                var tokenValidationParams = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("1E64F13A4162FC6FCBA0877C102CEAE68CC870FB7D5A3672F9F5C930F1F125DF")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var principal = jwtHandler.ValidateToken(token, tokenValidationParams, out var validatedToken);

                var privilegeTypeClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (privilegeTypeClaim != RequiredPrivilegeType)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                    { "Controller", "Auth" },
                    { "Action", "AccessDenied" }
                        });
                }
            }
            catch (SecurityTokenException)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                { "Controller", "Auth" },
                { "Action", "AccessDenied" }
                    });
            }
        }
    }
}
