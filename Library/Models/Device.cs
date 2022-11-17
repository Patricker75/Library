using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Device
	{
		public int ID { get; set; }

		[Column("item_type")]
		public ItemType ItemType { get; set; }

		[Required]
		[MaxLength(100)]
		[Column("name")]
		public string Name { get; set; }

		[Column("condition")]
		public Condition Condition { get; set; }
	}
}