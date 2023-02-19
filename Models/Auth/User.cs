using System.ComponentModel.DataAnnotations;

namespace icounselvault.Models.Auth
{
    public class User
    {
        [Key]
        public int USER_ID { get; set; }
        [Required]
        public string USERNAME { get; set; }
        [Required]
        public string PASSWORD { get; set; }
        [Required]
        public string USER_STATUS { get; set; } = "ACT";
        [Required]
        public string PRIVILEGE_TYPE { get; set; }
    }
}
