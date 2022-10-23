using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Relationships
{
    public class Accesses
    {
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }

        [ForeignKey("Member")]
        [Column("member_id")]
        public int MemberID { get; set; }

        [ForeignKey("ResearchResource")]
        [Column("resource_id")]
        public int ResourceID { get; set; }
    }
}
