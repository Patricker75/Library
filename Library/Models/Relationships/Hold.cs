using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Relationships
{
    public class Hold
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime HoldDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PickUpDate { get; set; }

        [Required]
        [Column("waiting")]
        public bool IsWaiting { get; set; }

        [ForeignKey("book")]
        [Column("book_id")]
        public int BookID { get; set; }

        [ForeignKey("member")]
        [Column("member_id")]
        public int MemberID { get; set; }
    }
}
