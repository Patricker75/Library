using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Relationships
{
    public class Uses
    {
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        [ForeignKey("Member")]
        [Column("member_id")]
        public int MemberID { get; set; }

        [ForeignKey("Service")]
        [Column("service_id")]
        public int ServiceID { get; set; }
    }
}
