using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Author
	{
		public int ID { get; set; }

		[MaxLength(20)]
		[Column("first_name")]
		public string FirstName { get; set; }

		[MaxLength(1)]
		[Column("middle_name")]
		public string? MiddleName { get; set; }

		[MaxLength(20)]
		[Column("last_name")]
		public string LastName { get; set; }

		public bool IsValid ()
		{
			return !(string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName));
		}

		public bool IsEmpty()
		{
			return string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName);
		}
	}
}