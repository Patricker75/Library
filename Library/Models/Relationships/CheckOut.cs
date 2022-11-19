using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Relationships
{
    [Table("check_out")]
    public class CheckOut
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("check_out_date")]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("due_date")]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Date)]
        [Column("return_date")]
        public DateTime? ReturnDate { get; set; }

        [Required]
        [Column("returned")]
        public bool IsReturned { get; set; }

        [Required]
        [Column("item_type")]
        public ItemType Type { get; set; }

        [Required]
        [Column("item_id")]
        public int ItemID { get; set; }

        [ForeignKey("member")]
        [Column("member_id")]
        public int MemberID { get; set; }
    }
}
