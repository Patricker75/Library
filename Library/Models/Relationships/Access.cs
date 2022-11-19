using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Relationships
{
    [Table("access")]
    public class Access
    {
        [Required]
        [DataType(DataType.DateTime)]
        [Column("time_stamp")]
        public DateTime TimeStamp { get; set; }

        [ForeignKey("resource")]
        [Column("resource_id")]
        public int ResourceID { get; set; }

        [ForeignKey("member")]
        [Column("member_id")]
        public int MemberID { get; set; }
    }
}
