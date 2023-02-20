using icounselvault.Models.Profiles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace icounselvault.Models.Counseling
{
    public class CounselDataInsertRequest
    {
        [Key]
        public int COUNSEL_DATA_INSERT_REQUEST_ID { get; set; }

        //Counseling Data foreign key
        [ForeignKey("ClientGuidanceHistory")]
        public ClientGuidanceHistory clientGuidanceHistory { get; set; }

        //Client foreign key
        [ForeignKey("Client")]
        public Client client { get; set; }

        //Counselor foreign key
        [ForeignKey("Counselor")]
        public Counselor? counselor { get; set; }

        [Required]
        public string INSERT_REQUEST_STATUS { get; set; } = "PENDING";
        public string? INSERT_REQUEST_REMARK { get; set; }
        [Required]
        public DateTime CREATED_DATE { get; set; }
    }
}
