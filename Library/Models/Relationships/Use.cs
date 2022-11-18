using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Relationships
{
    public class Use
    {
        [Required]
        [DataType(DataType.DateTime)]
        [Column("time_stamp")]
        public DateTime TimeStamp { get; set; }

        [ForeignKey("service")]
        [Column("service_id")]
        public int ServiceID { get; set; }

        [ForeignKey("member")]
        [Column("member_id")]
        public int MemberID { get; set; }
    }
}
