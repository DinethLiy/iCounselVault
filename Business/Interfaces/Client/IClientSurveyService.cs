namespace icounselvault.Business.Interfaces.Client
{
    public interface IClientSurveyService
    {
        string GetClientSurveyResult(string accessToken, string leader1, string leader2, string leader3,
                string introv1, string introv2, string introv3,
                string agreeable1, string agreeable2, string agreeable3,
                string consc1, string consc2, string consc3,
                string emotion1, string emotion2, string emotion3,
                string creative1, string creative2, string creative3,
                string loyal1, string loyal2, string loyal3,
                string kind1, string kind2, string kind3,
                string curious1, string curious2, string curious3);
    }
}
