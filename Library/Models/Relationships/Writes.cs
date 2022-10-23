using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Relationships
{
    public class Writes
    {
        [ForeignKey("Book")]
        [Column("book_id")]
        public int BookID { get; set; }

        [ForeignKey("Author")]
        [Column("author_id")]
        public int AuthorID { get; set; }
    }
}
