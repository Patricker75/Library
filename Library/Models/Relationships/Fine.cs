using Library.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Relationships
{
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
