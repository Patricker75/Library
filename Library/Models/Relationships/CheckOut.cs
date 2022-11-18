using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Relationships
{
    public class CheckOut
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("check_out_date")]
        public DateTime CheckOutDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; }

        [Required]
        public bool IsReturned { get; set; }

        [Required]
        public ItemType Type { get; set; }

        [ForeignKey("member")]
        [Column("member_id")]
        public int MemberID { get; set; }

        [ForeignKey("book")]
        [Column("book_id")]
        public int BookID { get; set; }

        [ForeignKey("device")]
        [Column("device_id")]
        public int DeviceID { get; set; }
    }
}
