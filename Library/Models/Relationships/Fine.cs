using Library.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Relationships
{
    [Table("fine")]
    public class Fine
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("fine_date")]
        public DateTime FineDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PaidDate { get; set; }

        [Required]
        public ItemType Type { get; set; }

        [Required]
        public int ItemID { get; set; }

        [ForeignKey("member")]
        [Column("member_id")]
        public int MemberID { get; set; }
    }
}
