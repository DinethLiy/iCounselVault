using icounselvault.Models.Profiles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace icounselvault.Models.Counseling
{
    public class CounselRequest
    {
        [Key]
        public int COUNSEL_REQUEST_ID { get; set; }

        //Client foreign key
        [ForeignKey("Client")]
        public Client client { get; set; }

        //Counselor foreign key
        [ForeignKey("Counselor")]
        public Counselor counselor { get; set; }

        //These 2 attributes define the date range for counseling that the client would prefer
        [Required]
        public DateTime FROM_DATE { get; set; }
        [Required]
        public DateTime TO_DATE { get; set;}

        [Required]
        public string COUNSEL_REQUEST_STATUS { get; set; } = "PENDING";
        public string? COUNSEL_REQUEST_REMARK { get; set; }
        [Required]
        public DateTime CREATED_DATE { get; set; }
    }
}
