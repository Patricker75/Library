using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Author
	{
		public int ID { get; set; }

		[Column("first_name")]
		public string FirstName { get; set; }

		[Column("middle_initial")]
		public string? MiddleName { get; set; }

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