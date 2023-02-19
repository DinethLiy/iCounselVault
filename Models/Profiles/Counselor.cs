using icounselvault.Models.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace icounselvault.Models.Profiles
{
    public class Counselor
    {
        [Key]
        public int COUNSELOR_ID { get; set; }

        //User foreign key
        [ForeignKey("User")]
        public User user { get; set; }

        [Required]
        public string NAME { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string GENDER { get; set; }
        [Required]
        public string NIC { get; set; }
        public string? ADDRESS { get; set; }
        [Required]
        public string COUNTRY { get; set; }
        public string? CONTACT_NUM { get; set; }
        [Required]
        public string EMAIL { get; set; }
        [Required]
        public string COUNSELOR_STATUS { get; set; } = "ACT";
    }
}
