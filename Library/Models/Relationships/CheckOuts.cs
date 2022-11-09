using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Relationships
{
    [Table("check_outs")]
    public class CheckOuts
    {
        public bool Returned { get; set; }

        [DataType(DataType.Date)]
        [Column("return_date")]
        public DateTime ReturnDate { get; set; }

        [ForeignKey("Member")]
        [Column("member_id")]
        public int MemberID { get; set; }

        [ForeignKey("Book")]
        [Column("book_id")]
        public int BookID { get; set; }
    }
}
