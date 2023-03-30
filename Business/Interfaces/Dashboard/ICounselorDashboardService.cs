using System.Collections;

namespace icounselvault.Business.Interfaces.Dashboard
{
    public interface ICounselorDashboardService
    {
        ArrayList GetCounselorDashboardData(string accessToken);
    }
}
