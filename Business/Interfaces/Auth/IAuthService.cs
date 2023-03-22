using icounselvault.Models.Auth;

namespace icounselvault.Business.Interfaces.Auth
{
    public interface IAuthService
    {
        User? GetLoggedInUser(string accessToken);
    }
}
