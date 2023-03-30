using icounselvault.Business.Interfaces.Auth;
using icounselvault.Business.Interfaces.Dashboard;
using icounselvault.Models.Profiles;
using icounselvault.Utility;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace icounselvault.Business.Services.Dashboard
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly AppDbContext _context;

        public AdminDashboardService(AppDbContext context)
        {
            _context = context;
        }

        public ArrayList GetDataForAdminDashboard() 
        {
            ArrayList resultList = new() 
            {
                GetPendingCounselRequests(),
                GetPendingInsertRequests(),
                GetClientCount(),
                GetCounselorCount()
            };
            return resultList;
        }

        private string GetPendingInsertRequests()
        {
            return _context.COUNSEL_DATA_INSERT_REQUEST
                .Where(cdir => cdir.INSERT_REQUEST_STATUS == "PENDING")
                .Count().ToString();
        }

        private string GetPendingCounselRequests()
        {
            return _context.COUNSEL_REQUEST
                .Where(cr => cr.COUNSEL_REQUEST_STATUS == "PENDING")
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
