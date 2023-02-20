using icounselvault.Models.Profiles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace icounselvault.Models.Counseling
{
    public class ClientGuidanceHistory
    {
        [Key]
        public int CLIENT_GUIDANCE_HISTORY_ID { get; set; }

        //Client foreign key
        [ForeignKey("Client")]
        public Client client { get; set; }

        //Counselor foreign key
        [ForeignKey("Counselor")]
        public Counselor? counselor { get; set; }

        public string? EXTERNAL_GUIDANCE_SOURCE { get; set; }
        [Required]
        public string GUIDANCE_ADVICE { get; set; }
        [Required]
        public string CLIENT_SATISFACTION { get; set; }
        [Required]
        public string HISTORY_STATUS { get; set; } = "ACT";
        [Required]
        public DateTime CREATED_DATE { get; set; }
    }
}
