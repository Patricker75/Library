using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Book
	{
		public int ID { get; set; }

<<<<<<< HEAD
		[Column("title")]
=======
		[Required]
		[MaxLength(125)]
>>>>>>> model
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

<<<<<<< HEAD
		[Column("condition")]
		public Condition Condition { get; set; }

		[Column("audience")]
		public Audience Audience { get; set; }
=======
		[Required]
		[Column("audience")]
		public Audience Audience { get; set; }

		[Required]
		public int Quantity { get; set; }

		[DataType(DataType.Date)]
		public DateTime DateAdded { get; set; }

		[ForeignKey("author")]
		public int AuthorID { get; set; }

		[ForeignKey("publisher")]
		public int PublisherID { get; set; }
>>>>>>> model
	}
}