using icounselvault.Business.Interfaces.Auth;
using icounselvault.Business.Interfaces.Dashboard;
using icounselvault.Models.Profiles;
using icounselvault.Utility;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace icounselvault.Business.Services.Dashboard
{
    public class CounselorDashboardService : ICounselorDashboardService
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;
        private Counselor foundCounselor;

        public CounselorDashboardService(AppDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public ArrayList GetCounselorDashboardData(string accessToken) 
        {
            SetFoundCounselor(accessToken);
            ArrayList resultList = new()
            {
                GetPendingCounselRequests(),
                GetPendingInsertRequests(),
                GetCounselorGuidanceRecords(),
                GetCounselorCounselRequests(),
                foundCounselor.NAME
            };
            return resultList;
        }

        private string GetPendingCounselRequests() 
        {
            return _context.COUNSEL_REQUEST
                .Include(cr => cr.counselor)
                .Where(cr => cr.counselor == foundCounselor
                          && cr.COUNSEL_REQUEST_STATUS == "PENDING")
                .Count().ToString();
        }

        private string GetPendingInsertRequests() 
        {
            return _context.COUNSEL_DATA_INSERT_REQUEST
                .Include(cdir => cdir.counselor)
                .Where(cdir => cdir.counselor == foundCounselor
                            && cdir.INSERT_REQUEST_STATUS == "PENDING")
                .Count().ToString();
        }

        private string GetCounselorGuidanceRecords()
        {
            return _context.CLIENT_GUIDANCE_HISTORY
                .Include(cgh => cgh.counselor)
                .Where(cgh => cgh.counselor == foundCounselor && cgh.HISTORY_STATUS == "ACT")
                .Count().ToString();
        }

        private string GetCounselorCounselRequests()
        {
            return _context.COUNSEL_REQUEST
                .Include(cr => cr.counselor)
                .Where(cr => cr.counselor == foundCounselor && cr.COUNSEL_REQUEST_STATUS != "REJECTED")
                .Count().ToString();
        }

        private void SetFoundCounselor(string accessToken)
        {
            var foundUser = _authService.GetLoggedInUser(accessToken);
            foundCounselor = _context.COUNSELOR
                .Include(co => co.user)
                .Where(co => co.user == foundUser)
                .FirstOrDefault();
        }
    }
}
