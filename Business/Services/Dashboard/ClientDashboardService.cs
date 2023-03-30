using icounselvault.Business.Interfaces.Auth;
using icounselvault.Business.Interfaces.Dashboard;
using icounselvault.Utility;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace icounselvault.Business.Services.Dashboard
{
    public class ClientDashboardService : IClientDashboardService
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;
        private Models.Profiles.Client foundClient;

        public ClientDashboardService(AppDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public ArrayList GetClientDashboardData(string accessToken) 
        {
            SetFoundClient(accessToken);
            ArrayList resultList = new()
            {
                GetClientInsertRequests(),
                GetPendingInsertRequests(),
                GetClientGuidanceRecords(),
                GetClientCounselRequests(),
                foundClient.NAME
            };
            return resultList;
        }

        private string GetClientInsertRequests() 
        {
            return _context.COUNSEL_DATA_INSERT_REQUEST
                .Include(cdir => cdir.client)
                .Include(cdir => cdir.counselor)
                .Where(cdir => cdir.client == foundClient
                            && cdir.counselor != null
                            && cdir.INSERT_REQUEST_STATUS == "PENDING")
                .Count().ToString();
        }

        private string GetPendingInsertRequests() 
        {
            return _context.COUNSEL_DATA_INSERT_REQUEST
                .Include(cdir => cdir.client)
                .Include(cdir => cdir.clientGuidanceHistory)
                .Where(cdir => cdir.client == foundClient
                            && cdir.clientGuidanceHistory.EXTERNAL_GUIDANCE_SOURCE != null
                            && cdir.INSERT_REQUEST_STATUS == "PENDING")
                .Count().ToString();
        }

        private string GetClientGuidanceRecords() 
        {
            return _context.CLIENT_GUIDANCE_HISTORY
                .Include(cgh => cgh.client)
                .Where(cgh => cgh.client == foundClient && cgh.HISTORY_STATUS == "ACT")
                .Count().ToString();
        }

        private string GetClientCounselRequests() 
        {
            return _context.COUNSEL_REQUEST
                .Include(cr => cr.client)
                .Where(cr => cr.client == foundClient && cr.COUNSEL_REQUEST_STATUS != "REJECTED")
                .Count().ToString();
        }

        private void SetFoundClient(string accessToken) 
        {
            var foundUser = _authService.GetLoggedInUser(accessToken);
            foundClient = _context.CLIENT
                .Include(cl => cl.user)
                .Where(cl => cl.user == foundUser)
                .FirstOrDefault();
        }
    }
}
