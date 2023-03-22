using icounselvault.Business.Interfaces.Auth;
using icounselvault.Business.Interfaces.Client;
using icounselvault.Utility;
using Microsoft.EntityFrameworkCore;

namespace icounselvault.Business.Services.Client
{
    public class ClientSurveyService : IClientSurveyService
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;

        public ClientSurveyService(AppDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public string GetClientSurveyResult(string accessToken, string leader1, string leader2, string leader3,
                string introv1, string introv2, string introv3,
                string agreeable1, string agreeable2, string agreeable3,
                string consc1, string consc2, string consc3,
                string emotion1, string emotion2, string emotion3,
                string creative1, string creative2, string creative3,
                string loyal1, string loyal2, string loyal3,
                string kind1, string kind2, string kind3,
                string curious1, string curious2, string curious3)
        {
            // Get Agree/Disagree levels for each personality trait
            string leaderResult = GetTraitLevel(leader1[^1].ToString(), leader2[^1].ToString(), leader3[^1].ToString());
            string introvResult = GetTraitLevel(introv1[^1].ToString(), introv2[^1].ToString(), introv3[^1].ToString());
            string agreeableResult = GetTraitLevel(agreeable1[^1].ToString(), agreeable2[^1].ToString(), agreeable3[^1].ToString());
            string conscResult = GetTraitLevel(consc1[^1].ToString(), consc2[^1].ToString(), consc3[^1].ToString());
            string emotionResult = GetTraitLevel(emotion1[^1].ToString(), emotion2[^1].ToString(), emotion3[^1].ToString());
            string creativeResult = GetTraitLevel(creative1[^1].ToString(), creative2[^1].ToString(), creative3[^1].ToString());
            string loyalResult = GetTraitLevel(loyal1[^1].ToString(), loyal2[^1].ToString(), loyal3[^1].ToString());
            string kindResult = GetTraitLevel(kind1[^1].ToString(), kind2[^1].ToString(), kind3[^1].ToString());
            string curiousResult = GetTraitLevel(curious1[^1].ToString(), curious2[^1].ToString(), curious3[^1].ToString());

            // Create Full Survey result string
            string finalSurveyResult = GetLeaderString(leaderResult) + GetIntrovString(introvResult) + GetAgreeableString(agreeableResult)
                                        + GetConscString(conscResult) + GetEmotionString(emotionResult) + GetCreativeString(creativeResult)
                                        + GetLoyalString(loyalResult) + GetKindString(kindResult) + GetCuriousString(curiousResult);
            // Handle an "All Neutral" instance
            if (finalSurveyResult == "") 
            {
                finalSurveyResult = "Neutral in all aspects";
            }

            // Save Survey result
            var foundUser = _authService.GetLoggedInUser(accessToken);
            var foundClient = _context.CLIENT
                .Include(cl => cl.user)
                .Where(cl => cl.user == foundUser)
                .FirstOrDefault();
            var clientExperience = _context.CLIENT_EXPERIENCE
                .Include(cle => cle.client)
                .Where(cle => cle.client == foundClient)
                .FirstOrDefault();
            clientExperience.SURVEY_RESULT = finalSurveyResult;
            _context.SaveChanges();

            // Return Survey result
            return finalSurveyResult;
        }

        // Gets the Agree/Disagree level for the given personality trait
        private static string GetTraitLevel(string traitAnswer1, string traitAnswer2, string traitAnswer3) 
        {
            var resultForAnswerCombinations = new Dictionary<(string, string, string), string>
            {
                { ("A", "A", "A"), "strongly agree" },
                { ("A", "A", "B"), "strongly agree" },
                { ("A", "B", "A"), "strongly agree" },
                { ("B", "A", "A"), "strongly agree" },

                { ("A", "A", "C"), "agree" },
                { ("A", "C", "A"), "agree" },
                { ("C", "A", "A"), "agree" },
                { ("A", "B", "B"), "agree" },
                { ("B", "A", "B"), "agree" },
                { ("B", "B", "A"), "agree" },

                { ("A", "B", "C"), "neutral" },
                { ("A", "C", "B"), "neutral" },
                { ("B", "A", "C"), "neutral" },
                { ("B", "C", "A"), "neutral" },
                { ("C", "A", "B"), "neutral" },
                { ("C", "B", "A"), "neutral" },
                { ("B", "B", "B"), "neutral" },

                { ("C", "C", "A"), "disagree" },
                { ("C", "A", "C"), "disagree" },
                { ("A", "C", "C"), "disagree" },
                { ("C", "B", "B"), "disagree" },
                { ("B", "C", "B"), "disagree" },
                { ("B", "B", "C"), "disagree" },

                { ("B", "C", "C"), "strongly disagree" },
                { ("C", "B", "C"), "strongly disagree" },
                { ("C", "C", "B"), "strongly disagree" },
                { ("C", "C", "C"), "strongly disagree" },
            };

            var answerCombination = (traitAnswer1, traitAnswer2, traitAnswer3);
            if (resultForAnswerCombinations.TryGetValue(answerCombination, out var result))
            {
                return result;
            }
            else 
            {
                return "error";
            }
        }

        // Provides a specific output based on the personality trait and the given agree/disagree level
        private static string GetLeaderString(string leaderResult)
        {
            if (leaderResult == "strongly agree")
            {
                return "Strong leader,";
            }
            else if (leaderResult == "agree")
            {
                return "Aspiring leader,";
            }
            else
            {
                return "";
            }
        }

        private static string GetIntrovString(string introvResult)
        {
            if (introvResult == "strongly agree")
            {
                return "Highly introverted,";
            }
            else if (introvResult == "agree")
            {
                return "Slightly introverted,";
            }
            else if (introvResult == "disagree")
            {
                return "Slightly extroverted,";
            }
            else if (introvResult == "strongly disagree")
            {
                return "Highly extroverted,";
            }
            else
            {
                return "";
            }
        }

        private static string GetAgreeableString(string agreeableResult)
        {
            if (agreeableResult == "strongly agree")
            {
                return "Highly agreeable,";
            }
            else if (agreeableResult == "agree")
            {
                return "Agreeable,";
            }
            else if (agreeableResult == "disagree" || agreeableResult == "strongly disagree")
            {
                return "Not very agreeable,";
            }
            else
            {
                return "";
            }
        }

        private static string GetConscString(string conscResult)
        {
            if (conscResult == "strongly agree")
            {
                return "Highly conscientious,";
            }
            else if (conscResult == "agree")
            {
                return "Conscientious,";
            }
            else if (conscResult == "disagree")
            {
                return "Impulsive and spontaneous,";
            }
            else if (conscResult == "strongly disagree")
            {
                return "Highly spontaneous,";
            }
            else
            {
                return "";
            }
        }

        private static string GetEmotionString(string emotionResult)
        {
            if (emotionResult == "strongly agree")
            {
                return "High emotional stability,";
            }
            else if (emotionResult == "agree")
            {
                return "Emotionally stable,";
            }
            else if (emotionResult == "disagree")
            {
                return "Emotionally sensitive,";
            }
            else if (emotionResult == "strongly disagree")
            {
                return "Highly reactive to emotions,";
            }
            else
            {
                return "";
            }
        }

        private static string GetCreativeString(string creativeResult)
        {
            if (creativeResult == "strongly agree")
            {
                return "Highly creative,";
            }
            else if (creativeResult == "agree")
            {
                return "Creative,";
            }
            else if (creativeResult == "disagree")
            {
                return "Conventional,";
            }
            else if (creativeResult == "strongly disagree")
            {
                return "Highly conventional,";
            }
            else
            {
                return "";
            }
        }

        private static string GetLoyalString(string loyalResult)
        {
            if (loyalResult == "strongly agree")
            {
                return "Highly loyal,";
            }
            else if (loyalResult == "agree")
            {
                return "Loyal,";
            }
            else if (loyalResult == "disagree")
            {
                return "Independent,";
            }
            else if (loyalResult == "strongly disagree")
            {
                return "Highly independent,";
            }
            else
            {
                return "";
            }
        }

        private static string GetKindString(string kindResult)
        {
            if (kindResult == "strongly agree")
            {
                return "Very kind,";
            }
            else if (kindResult == "agree")
            {
                return "Kind,";
            }
            else
            {
                return "";
            }
        }

        private static string GetCuriousString(string curiousResult)
        {
            if (curiousResult == "strongly agree")
            {
                return "Highly curious,";
            }
            else if (curiousResult == "agree")
            {
                return "Curious,";
            }
            else if (curiousResult == "disagree" || curiousResult == "strongly disagree")
            {
                return "Content with what is known,";
            }
            else
            {
                return "";
            }
        }
    }
}
