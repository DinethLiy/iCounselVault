using icounselvault.Business.Interfaces.Dashboard;
using icounselvault.Utility;
using System.Collections;

namespace icounselvault.Business.Services.Dashboard
{
    public class SuperAdminDashboardService : ISuperAdminDashboardService
    {
        private readonly AppDbContext _context;

        public SuperAdminDashboardService(AppDbContext context)
        {
            _context = context;
        }

        public ArrayList GetDataForSuperAdminDashboard() 
        {
            ArrayList resultList = new()
            {
                GetSuperAdminCount(),
                GetAdminCount(),
                GetClientCount(),
                GetCounselorCount()
            };
            return resultList;
        }

        private string GetSuperAdminCount() 
        {
            return _context.USER
                .Where(u => u.PRIVILEGE_TYPE == "SUPER_ADMIN" && u.USER_STATUS != "INA")
                .Count().ToString();
        }

        private string GetAdminCount()
        {
            return _context.USER
                .Where(u => u.PRIVILEGE_TYPE == "ADMIN" && u.USER_STATUS != "INA")
                .Count().ToString();
        }

        private string GetClientCount()
        {
            return _context.CLIENT
                .Where(cl => cl.CLIENT_STATUS != "INA")
                .Count().ToString();
        }

        private string GetCounselorCount()
        {
            return _context.COUNSELOR
                .Where(co => co.COUNSELOR_STATUS != "INA")
                .Count().ToString();
        }
    }
}
