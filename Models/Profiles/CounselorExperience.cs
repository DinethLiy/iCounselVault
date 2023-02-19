using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace icounselvault.Models.Profiles
{
    public class CounselorExperience
    {
        [Key]
        public int COUNSELOR_EXPERIENCE_ID { get; set; }

        //Counselor foreign key
        [ForeignKey("Counselor")]
        public Counselor counselor { get; set; }

        [Required]
        public string SCHOOL_EXPERIENCE { get; set; }
        public string? HIGHER_EDU_EXPERIENCE { get; set; }
        public string? JOB_EXPERIENCE { get; set; }
    }
}
