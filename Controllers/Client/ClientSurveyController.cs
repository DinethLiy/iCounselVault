using icounselvault.Business.Interfaces.Client;
using Microsoft.AspNetCore.Mvc;

namespace icounselvault.Controllers.Client
{
    public class ClientSurveyController : Controller
    {
        private readonly IClientSurveyService _clientSurveyService;

        public ClientSurveyController(IClientSurveyService clientSurveyService)
        {
            _clientSurveyService = clientSurveyService;
        }

        public IActionResult Index() 
        {
            TempData["pageNumber"] = "1";
            return View("../../Views/Client/ClientSurvey/ClientSurveyQuestions");
        }

        [HttpPost]
        public IActionResult GetSurveyPage1Data(string leader2, string agreeable1, string loyal3,
            string loyal1, string consc2, string consc3, string agreeable3)
        {
            List<string> answersForPage2 = new()
            {
                leader2,
                agreeable1,
                loyal3,
                loyal1,
                consc2,
                consc3,
                agreeable3
            };

            ViewData["answersForPage2"] = answersForPage2;
            TempData["pageNumber"] = "2";
            return View("../../Views/Client/ClientSurvey/ClientSurveyQuestions");
        }

        [HttpPost]
        public IActionResult GetSurveyPage2Data(string leader2, string agreeable1, string loyal3,
            string loyal1, string consc2, string consc3,
            string agreeable3, string curious1, string leader3,
            string consc1, string leader1, string creative2, string kind3, string introv2)
        {
            List<string> answersForPage3 = new()
            {
                leader2,
                agreeable1,
                loyal3,
                loyal1,
                consc2,
                consc3,
                agreeable3,
                curious1,
                leader3,
                consc1,
                leader1,
                creative2,
                kind3,
                introv2
            };

            ViewData["answersForPage3"] = answersForPage3;
            TempData["pageNumber"] = "3";
            return View("../../Views/Client/ClientSurvey/ClientSurveyQuestions");
        }

        [HttpPost]
        public IActionResult GetSurveyPage3Data(string leader2, string agreeable1, string loyal3,
            string loyal1, string consc2, string consc3,
            string agreeable3, string curious1, string leader3,
            string consc1, string leader1, string creative2,
            string kind3, string introv2, string emotion1,
            string curious2, string kind1, string loyal2,
            string kind2, string emotion2, string introv1)
        {
            List<string> answersForPage4 = new()
            {
                leader2,
                agreeable1,
                loyal3,
                loyal1,
                consc2,
                consc3,
                agreeable3,
                curious1,
                leader3,
                consc1,
                leader1,
                creative2,
                kind3,
                introv2,
                emotion1,
                curious2,
                kind1,
                loyal2,
                kind2,
                emotion2,
                introv1,
            };

            ViewData["answersForPage4"] = answersForPage4;
            TempData["pageNumber"] = "4";
            return View("../../Views/Client/ClientSurvey/ClientSurveyQuestions");
        }

        [HttpPost]
        public RedirectToActionResult GetSurveyPage4Data(string leader2, string agreeable1, string loyal3,
            string loyal1, string consc2, string consc3,
            string agreeable3, string curious1, string leader3,
            string consc1, string leader1, string creative2,
            string kind3, string introv2, string emotion1,
            string curious2, string kind1, string loyal2,
            string kind2, string emotion2, string introv1,
            string curious3, string creative1, string introv3,
            string creative3, string agreeable2, string emotion3)
        {
            string accessToken = Request.Cookies["access_token"];
            // Service call
            TempData["surveyResult"] = _clientSurveyService.GetClientSurveyResult(accessToken, leader1, leader2, leader3,
                introv1, introv2, introv3, agreeable1, agreeable2, agreeable3, consc1, consc2, consc3, emotion1, emotion2, emotion3,
                creative1, creative2, creative3, loyal1, loyal2, loyal3, kind1, kind2, kind3, curious1, curious2, curious3);
            return RedirectToAction("ShowClientExperience", "ClientExperience");
        }
    }
}
