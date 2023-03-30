using icounselvault.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace icounselvault.Models.Profiles
{
    public class Client
    {
        [Key]
        public int CLIENT_ID { get; set; }

        //User foreign key
        [ForeignKey("User")]
        public User user { get; set; }

        [Required]
        public Guid CLIENT_CODE { get; set; }
        [Required]
        public string NAME { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string GENDER { get; set; }
        public string? NIC { get; set; }
        public string? ADDRESS { get; set; }
        [Required]
        public string COUNTRY { get; set; }
        public string? CONTACT_NUM { get; set; }
        [Required]
        public string EMAIL { get; set; }
        [Required]
        public string CLIENT_STATUS { get; set; } = "ACT";
    }
}
