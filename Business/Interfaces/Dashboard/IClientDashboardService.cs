using System.Collections;

namespace icounselvault.Business.Interfaces.Dashboard
{
    public interface IClientDashboardService
    {
        ArrayList GetClientDashboardData(string accessToken);
    }
}
