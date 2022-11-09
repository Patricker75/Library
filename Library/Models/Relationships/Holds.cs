using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Relationships
{
    public class Holds
    {
        public bool Waiting { get; set; }

        [DataType(DataType.Date)]
        [Column("hold_date")]
        public DateTime HoldDate { get; set; }

        [ForeignKey("Member")]
        [Column("member_id")]
        public int MemberID { get; set; }

        [ForeignKey("Book")]
        [Column("book_title")]
        public string BookTitle { get; set; }

        [ForeignKey("Author")]
        [Column("author_id")]
        public int AuthorID { get; set; }
    }
}
