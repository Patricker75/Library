using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Service
	{
		public int ID { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[Column("location")]
		public string Location { get; set; }

		[Column("availability")]
		public bool IsAvailable { get; set; }
	}
}