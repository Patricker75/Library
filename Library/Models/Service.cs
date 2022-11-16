using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Service
	{
		public int ID { get; set; }

		[Required]
		[MaxLength(50)]
		[Column("name")]
		public string Name { get; set; }

        [Required]
		[MaxLength(10)]
        [Column("location")]
		public string Location { get; set; }

		[Column("availability")]
		public bool IsAvailable { get; set; }
	}
}