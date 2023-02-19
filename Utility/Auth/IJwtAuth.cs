using icounselvault.Models.Auth;

namespace icounselvault.Utility.Auth
{
    public interface IJwtAuth
    {
        string Authentication(User user);
    }
}
