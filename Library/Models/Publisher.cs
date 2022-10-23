using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Publisher
	{
		public int ID { get; set; }

		[Column("name")]
		public string Name { get; set; }
	}
}