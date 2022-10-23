using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Relationships
{
    public class Publishes
    {
        [ForeignKey("Book")]
        [Column("book_id")]
        public int BookID { get; set; }

        [ForeignKey("Publisher")]
        [Column("publisher_id")]
        public int PublisherID { get; set; }
    }
}
