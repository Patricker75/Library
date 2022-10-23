using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Relationships
{
    public class Holds
    {
        private bool Waiting { get; set; }

        [DataType(DataType.Date)]
        [Column("hold_date")]
        public DateTime HoldDate { get; set; }

        [ForeignKey("Member")]
        [Column("member_id")]
        public int MemberID { get; set; }

        [ForeignKey("Book")]
        [Column("book_id")]
        public int BookID { get; set; }
    }
}
