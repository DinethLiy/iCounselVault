using icounselvault.Business.Interfaces.Admin;
using Microsoft.AspNetCore.Mvc;

namespace icounselvault.Controllers.Admin
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [Route("Reports/Generate-Survey-Usage-Report")]
        public IActionResult ViewSurveyCountReport()
        {
            return View("../../Views/Admin/Reports/SurveyCountReport");
        }

        [Route("Reports/Generate-Counsel-Request-Report")]
        public IActionResult ViewCounselRequestsReport()
        {
            return View("../../Views/Admin/Reports/CounselRequestsReport");
        }

        [Route("Reports/Generate-Data-Request-Report")]
        public IActionResult ViewDataInsertRequestsReport()
        {
            return View("../../Views/Admin/Reports/DataInsertRequestsReport");
        }

        [Route("Reports/Generate-Counselor-Activity-Report")]
        public IActionResult ViewCounselorActivityReport()
        {
            return View("../../Views/Admin/Reports/CounselorActivityReport");
        }

        [Route("Reports/Generate-Client-Activity-Report")]
        public IActionResult ViewClientActivityReport()
        {
            return View("../../Views/Admin/Reports/ClientActivityReport");
        }

        [HttpPost]
        public FileResult DownloadSurveyCountReport(string takenTimes, string country) 
        {
            var stream = _reportService.GenerateSurveyCountReport(country, takenTimes);
            return File(stream.ToArray(), "application/pdf", "Client Questionnaire Usage Report.pdf");
        }

        [HttpPost]
        public FileResult DownloadCounselRequestsReport(string createdAfter, string status)
        {
            var stream = _reportService.GenerateCounselRequestReport(status, createdAfter);
            return File(stream.ToArray(), "application/pdf", "Counsel Requests Report.pdf");
        }

        [HttpPost]
        public FileResult DownloadDataInsertRequestsReport(string createdAfter, string status)
        {
            var stream = _reportService.GenerateDataInsertRequestReport(status, createdAfter);
            return File(stream.ToArray(), "application/pdf", "Guidance Data Insert Requests Report.pdf");
        }

        [HttpPost]
        public FileResult DownloadCounselorActivityReport(string status, string country)
        {
            var stream = _reportService.GenerateCounselorActivityReport(country, status);
            return File(stream.ToArray(), "application/pdf", "Counselor Activity Report.pdf");
        }

        [HttpPost]
        public FileResult DownloadClientActivityReport(string status, string country)
        {
            var stream = _reportService.GenerateClientActivityReport(country, status);
            return File(stream.ToArray(), "application/pdf", "Client Questionnaire Usage Report.pdf");
        }
    }
}
