namespace icounselvault.Business.Interfaces.Admin
{
    public interface IReportService
    {
        MemoryStream GenerateSurveyCountReport(string country, string usageCount);
        MemoryStream GenerateCounselRequestReport(string status, string? createdAfter);
        MemoryStream GenerateDataInsertRequestReport(string status, string? createdAfter);
        MemoryStream GenerateCounselorActivityReport(string country, string status);
        MemoryStream GenerateClientActivityReport(string country, string status);
    }
}
