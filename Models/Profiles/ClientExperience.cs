using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace icounselvault.Models.Profiles
{
    public class ClientExperience
    {
        [Key]
        public int CLIENT_EXPERIENCE_ID { get; set; }

        //Counselor foreign key
        [ForeignKey("Client")]
        public Client client { get; set; }

        [Required]
        public string SCHOOL_EXPERIENCE { get; set; }
        public string? HIGHER_EDU_EXPERIENCE { get; set; }
        public string? JOB_EXPERIENCE { get; set; }
        public string? PREFERED_CAREER_FIELD { get; set; }
        public string? SURVEY_RESULT { get; set; }
    }
}
