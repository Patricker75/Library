using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("book")]
    public partial class Book
	{
		public int ID { get; set; }

		[Required]
		[MaxLength(125)]
		public string Title { get; set; }

		[Required]
		public Genre Genre { get; set; }

		[Required]
		[MaxLength(500)]
		[Column("summary")]
		public string Summary { get; set; }

		[Required]
		[MaxLength(15)]
		[Column("dewey_number")]
		public string DeweyNumber { get; set; }

		[Required]
		[Column("audience")]
		public Audience Audience { get; set; }

		[Range(0, int.MaxValue)]
		[Required]
		public int Quantity { get; set; }

        [Column("date_added")]
        [DataType(DataType.Date)]
		public DateTime DateAdded { get; set; }

		[Column("author_id")]
		[ForeignKey("author")]
		public int AuthorID { get; set; }

		[Column("publisher_id")]
		[ForeignKey("publisher")]
		public int PublisherID { get; set; }
	}
}