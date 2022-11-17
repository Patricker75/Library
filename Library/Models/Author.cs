using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Author
	{
		public int ID { get; set; }

		[Required]
		[MaxLength(20)]
		[Column("first_name")]
		public string FirstName { get; set; }

		[MaxLength(1)]
		[Column("middle_initial")]
		public string? MiddleName { get; set; }

		[Required]
		[MaxLength(20)]
		[Column("last_name")]
		public string LastName { get; set; }
	}
}