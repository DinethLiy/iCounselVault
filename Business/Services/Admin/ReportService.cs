using icounselvault.Business.Interfaces.Admin;
using icounselvault.Models.Counseling;
using icounselvault.Models.Profiles;
using icounselvault.Utility;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.EntityFrameworkCore;

namespace icounselvault.Business.Services.Admin
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;
        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public MemoryStream GenerateSurveyCountReport(string country, string usageCount)
        {
            List<ClientExperience> surveyData = GetDataForSurveyCountReport(country, usageCount);

            using var stream = new MemoryStream();
            using (var writer = new PdfWriter(stream))
            {
                using var pdf = new PdfDocument(writer);
                var document = new Document(pdf);
                var headerCellFont = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

                //If there is no survey data, the titles and table will not be printed
                if (surveyData.Count != 0)
                {
                    //Create report title and subtitles
                    document.Add(new Paragraph("Client Survey Usage Report").SetFontSize(24));
                    document.Add(new Paragraph("Date: " + DateTime.Now.ToString("dd/MM/yyyy")));
                    document.Add(new Paragraph("iCounselVault").SetFontSize(10));

                    //Create report table
                    var table = new Table(new float[] { 2, 2, 2 });
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Client Name").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("No. of Times the Survey has been taken").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Current Survey Result").SetFont(headerCellFont).SetFontSize(14)));
                    foreach (var survey in surveyData)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(survey.client.NAME)));
                        table.AddCell(new Cell().Add(new Paragraph(survey.SURVEY_TAKEN_AMOUNT.ToString())));
                        table.AddCell(new Cell().Add(new Paragraph(HandleNullStringsForReport(survey.SURVEY_RESULT))));
                    }
                    document.Add(table);
                }
                else
                {
                    document.Add(new Paragraph("No Survey data for the given parameters").SetFontSize(24));
                }

                document.Close();
            }

            return stream;
        }

        private List<ClientExperience> GetDataForSurveyCountReport(string country, string usageCount)
        {
            List<ClientExperience> resultList = _context.CLIENT_EXPERIENCE
                        .Include(cle => cle.client)
                        .ToList();
            if (country != "all")
            {
                resultList = resultList
                    .Where(cle => cle.client.COUNTRY == country)
                    .ToList();
            }
            if (int.Parse(usageCount) > 0) 
            {
                resultList = resultList
                    .Where(cle => cle.SURVEY_TAKEN_AMOUNT > int.Parse(usageCount))
                    .ToList();
            }
            return resultList;
        }

        public MemoryStream GenerateCounselRequestReport(string status, string? createdAfter)
        {
            List<CounselRequest> counselRequestData = GetDataForCounselRequestReport(status, createdAfter);

            using var stream = new MemoryStream();
            using (var writer = new PdfWriter(stream))
            {
                using var pdf = new PdfDocument(writer);
                var document = new Document(pdf);
                var headerCellFont = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

                //If there is no survey data, the titles and table will not be printed
                if (counselRequestData.Count != 0)
                {
                    //Create report title and subtitles
                    document.Add(new Paragraph("Counseling Requests Report").SetFontSize(24));
                    document.Add(new Paragraph("Date: " + DateTime.Now.ToString("dd/MM/yyyy")));
                    document.Add(new Paragraph("iCounselVault").SetFontSize(10));

                    //Create report table
                    var table = new Table(new float[] { 2, 2, 1, 2, 1, 2 });
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Client Name").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Counselor Name").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Created Date").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Requested Date Range").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Request status").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Remark").SetFont(headerCellFont).SetFontSize(14)));
                    foreach (var counselRequest in counselRequestData)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(counselRequest.client.NAME)));
                        table.AddCell(new Cell().Add(new Paragraph(counselRequest.counselor.NAME)));
                        table.AddCell(new Cell().Add(new Paragraph(counselRequest.CREATED_DATE.ToString("dd/MM/yyyy"))));
                        table.AddCell(new Cell().Add(new Paragraph(counselRequest.FROM_DATE.ToString("dd/MM/yyyy") + " - " + counselRequest.TO_DATE.ToString("dd/MM/yyyy"))));
                        table.AddCell(new Cell().Add(new Paragraph(counselRequest.COUNSEL_REQUEST_STATUS)));
                        table.AddCell(new Cell().Add(new Paragraph(HandleNullStringsForReport(counselRequest.COUNSEL_REQUEST_REMARK))));
                    }
                    document.Add(table);
                }
                else
                {
                    document.Add(new Paragraph("No Counsel Request data for the given parameters").SetFontSize(24));
                }

                document.Close();
            }

            return stream;
        }

        private List<CounselRequest> GetDataForCounselRequestReport(string status, string? createdAfter)
        {
            List<CounselRequest> resultList = _context.COUNSEL_REQUEST
                                    .Include(cr => cr.client)
                                    .Include(cr => cr.counselor)
                                    .ToList();
            if (status != "all")
            {
                resultList = resultList
                    .Where(cr => cr.COUNSEL_REQUEST_STATUS == status)
                    .ToList();
            }
            if (createdAfter != null)
            {
                resultList = resultList
                    .Where(cr => cr.CREATED_DATE > DateTime.Parse(createdAfter))
                    .ToList();
            }
            return resultList;
        }

        public MemoryStream GenerateDataInsertRequestReport(string status, string? createdAfter)
        {
            List<CounselDataInsertRequest> insertRequestData = GetDataForDataInsertRequestReport(status, createdAfter);

            using var stream = new MemoryStream();
            using (var writer = new PdfWriter(stream))
            {
                using var pdf = new PdfDocument(writer);
                var document = new Document(pdf);
                var headerCellFont = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

                //If there is no survey data, the titles and table will not be printed
                if (insertRequestData.Count != 0)
                {
                    //Create report title and subtitles
                    document.Add(new Paragraph("Guidance Advice Insert Requests Report").SetFontSize(24));
                    document.Add(new Paragraph("Date: " + DateTime.Now.ToString("dd/MM/yyyy")));
                    document.Add(new Paragraph("iCounselVault").SetFontSize(10));

                    //Create report table
                    var table = new Table(new float[] { 2, 2, 1, 2, 1, 2 });
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Client Name").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Counseling Source").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Created Date").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Guidance Advice").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Request status").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Remark").SetFont(headerCellFont).SetFontSize(14)));
                    foreach (var insertRequest in insertRequestData)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(insertRequest.client.NAME)));
                        table.AddCell(new Cell().Add(new Paragraph(PrintDataInsertRequestSource(insertRequest))));
                        table.AddCell(new Cell().Add(new Paragraph(insertRequest.CREATED_DATE.ToString("dd/MM/yyyy"))));
                        table.AddCell(new Cell().Add(new Paragraph(insertRequest.clientGuidanceHistory.GUIDANCE_ADVICE)));
                        table.AddCell(new Cell().Add(new Paragraph(insertRequest.INSERT_REQUEST_STATUS)));
                        table.AddCell(new Cell().Add(new Paragraph(HandleNullStringsForReport(insertRequest.INSERT_REQUEST_REMARK))));
                    }
                    document.Add(table);
                }
                else
                {
                    document.Add(new Paragraph("No Counsel Request data for the given parameters").SetFontSize(24));
                }

                document.Close();
            }

            return stream;
        }

        private List<CounselDataInsertRequest> GetDataForDataInsertRequestReport(string status, string? createdAfter)
        {
            List<CounselDataInsertRequest> resultList = _context.COUNSEL_DATA_INSERT_REQUEST
                        .Include(cdir => cdir.client)
                        .Include(cdir => cdir.counselor)
                        .Include(cdir => cdir.clientGuidanceHistory)
                        .ToList();
            if (status != "all")
            {
                resultList = resultList
                    .Where(cdir => cdir.INSERT_REQUEST_STATUS == status)
                    .ToList();
            }
            if (createdAfter != null)
            {
                resultList = resultList
                    .Where(cdir => cdir.CREATED_DATE > DateTime.Parse(createdAfter))
                    .ToList();
            }
            return resultList;
        }

        private static string PrintDataInsertRequestSource(CounselDataInsertRequest insertRequest)
        {
            if (insertRequest.counselor != null)
            {
                return "iCounselVault Counselor";
            }
            else
            {
                return insertRequest.clientGuidanceHistory.EXTERNAL_GUIDANCE_SOURCE;
            }
        }

        public MemoryStream GenerateCounselorActivityReport(string country, string status)
        {
            List<Counselor> counselorData = GetDataForCounselorActivityReport(country, status);

            using var stream = new MemoryStream();
            using (var writer = new PdfWriter(stream))
            {
                using var pdf = new PdfDocument(writer);
                var document = new Document(pdf);
                var headerCellFont = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

                //If there is no survey data, the titles and table will not be printed
                if (counselorData.Count != 0)
                {
                    //Create report title and subtitles
                    document.Add(new Paragraph("Counselor Activity Report").SetFontSize(24));
                    document.Add(new Paragraph("Date: " + DateTime.Now.ToString("dd/MM/yyyy")));
                    document.Add(new Paragraph("iCounselVault").SetFontSize(10));

                    //Create report table
                    var table = new Table(new float[] { 2, 1, 1, 1, 1, 2 });
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Counselor Name").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Country").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("No. of Counsel Requests").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("No. of Guidance Data Inserts").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Has Past Experience Record?").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Status").SetFont(headerCellFont).SetFontSize(14)));
                    foreach (var counselor in counselorData)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(counselor.NAME)));
                        table.AddCell(new Cell().Add(new Paragraph(counselor.COUNTRY)));
                        table.AddCell(new Cell().Add(new Paragraph(GetCounselorCounselRequestCount(counselor))));
                        table.AddCell(new Cell().Add(new Paragraph(GetCounselorInsertRequestCount(counselor))));
                        table.AddCell(new Cell().Add(new Paragraph(CheckCounselorPastExperience(counselor))));
                        table.AddCell(new Cell().Add(new Paragraph(counselor.COUNSELOR_STATUS)));
                    }
                    document.Add(table);
                }
                else
                {
                    document.Add(new Paragraph("No Counselor data for the given parameters").SetFontSize(24));
                }

                document.Close();
            }

            return stream;
        }

        private List<Counselor> GetDataForCounselorActivityReport(string country, string status)
        {
            List<Counselor> resultList = _context.COUNSELOR
                .ToList();
            if (country != "all")
            {
                resultList = resultList
                    .Where(co => co.COUNTRY == country)
                    .ToList();
            }
            if (status != "all")
            {
                resultList = resultList
                    .Where(co => co.COUNSELOR_STATUS == status)
                    .ToList();
            }
            return resultList;
        }

        private string GetCounselorCounselRequestCount(Counselor counselor)
        {
            return _context.COUNSEL_REQUEST
                .Include(cr => cr.counselor)
                .Where(cr => cr.counselor == counselor)
                .Count().ToString();
        }

        private string GetCounselorInsertRequestCount(Counselor counselor)
        {
            return _context.COUNSEL_DATA_INSERT_REQUEST
                .Include(cdir => cdir.counselor)
                .Where(cdir => cdir.counselor == counselor)
                .Count().ToString();
        }

        private string CheckCounselorPastExperience(Counselor counselor)
        {
            var pastExperience = _context.COUNSELOR_EXPERIENCE
                .Include(coe => coe.counselor)
                .Where(coe => coe.counselor == counselor)
                .FirstOrDefault();
            if (pastExperience != null)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }

        public MemoryStream GenerateClientActivityReport(string country, string status)
        {
            List<Models.Profiles.Client> clientData = GetDataForClientActivityReport(country, status);

            using var stream = new MemoryStream();
            using (var writer = new PdfWriter(stream))
            {
                using var pdf = new PdfDocument(writer);
                var document = new Document(pdf);
                var headerCellFont = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

                //If there is no survey data, the titles and table will not be printed
                if (clientData.Count != 0)
                {
                    //Create report title and subtitles
                    document.Add(new Paragraph("Client Activity Report").SetFontSize(24));
                    document.Add(new Paragraph("Date: " + DateTime.Now.ToString("dd/MM/yyyy")));
                    document.Add(new Paragraph("iCounselVault").SetFontSize(10));

                    //Create report table
                    var table = new Table(new float[] { 2, 1, 1, 1, 2, 1, 1, 2 });
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Client Name").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Country").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("No. of Counselor Requests").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("No. of Guidance Data Records").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Most Recent Guidance Record").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Record Inserted Date").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Has Past Experience Record?").SetFont(headerCellFont).SetFontSize(14)));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Status").SetFont(headerCellFont).SetFontSize(14)));
                    foreach (var client in clientData)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(client.NAME)));
                        table.AddCell(new Cell().Add(new Paragraph(client.COUNTRY)));
                        table.AddCell(new Cell().Add(new Paragraph(GetClientCounselRequestCount(client))));
                        table.AddCell(new Cell().Add(new Paragraph(GetClientInsertRequestCount(client))));
                        table.AddCell(new Cell().Add(new Paragraph(GetClientMostRecentGuidanceAdvice(client))));
                        table.AddCell(new Cell().Add(new Paragraph(GetClientMostRecentGuidanceDate(client))));
                        table.AddCell(new Cell().Add(new Paragraph(CheckClientPastExperience(client))));
                        table.AddCell(new Cell().Add(new Paragraph(client.CLIENT_STATUS)));
                    }
                    document.Add(table);
                }
                else
                {
                    document.Add(new Paragraph("No Counselor data for the given parameters").SetFontSize(24));
                }

                document.Close();
            }

            return stream;
        }

        private List<Models.Profiles.Client> GetDataForClientActivityReport(string country, string status)
        {
            List<Models.Profiles.Client> resultList = _context.CLIENT
                .ToList();
            if (country != "all")
            {
                resultList = resultList
                    .Where(cl => cl.COUNTRY == country)
                    .ToList();
            }
            if (status != "all")
            {
                resultList = resultList
                    .Where(cl => cl.CLIENT_STATUS == status)
                    .ToList();
            }
            return resultList;
        }

        private string GetClientCounselRequestCount(Models.Profiles.Client client)
        {
            return _context.COUNSEL_REQUEST
                .Include(cr => cr.client)
                .Where(cr => cr.client == client)
                .Count().ToString();
        }

        private string GetClientInsertRequestCount(Models.Profiles.Client client)
        {
            return _context.COUNSEL_DATA_INSERT_REQUEST
                .Include(cdir => cdir.client)
                .Where(cdir => cdir.client == client)
                .Count().ToString();
        }

        private string GetClientMostRecentGuidanceAdvice(Models.Profiles.Client client)
        {
            var foundGuidance = _context.CLIENT_GUIDANCE_HISTORY
                .Include(cgh => cgh.client)
                .Where(cgh => cgh.client == client)
                .OrderByDescending(cgh => cgh.CLIENT_GUIDANCE_HISTORY_ID)
                .FirstOrDefault();
            if (foundGuidance != null)
            {
                return foundGuidance.GUIDANCE_ADVICE;
            }
            else
            {
                return "";
            }
        }

        private string GetClientMostRecentGuidanceDate(Models.Profiles.Client client)
        {
            var foundGuidance = _context.CLIENT_GUIDANCE_HISTORY
                .Include(cgh => cgh.client)
                .Where(cgh => cgh.client == client)
                .OrderByDescending(cgh => cgh.CLIENT_GUIDANCE_HISTORY_ID)
                .FirstOrDefault();
            if (foundGuidance != null)
            {
                return foundGuidance.CREATED_DATE.ToString("dd/MM/yyyy");
            }
            else
            {
                return "";
            }
        }

        private string CheckClientPastExperience(Models.Profiles.Client client)
        {
            var pastExperience = _context.CLIENT_EXPERIENCE
                .Include(cle => cle.client)
                .Where(cle => cle.client == client)
                .FirstOrDefault();
            if (pastExperience != null)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }

        private static string HandleNullStringsForReport(string? givenString) 
        {
            if (givenString != null)
            {
                return givenString;
            }
            else 
            {
                return "";
            }
        }
    }
}
