using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("publisher")]
    public partial class Publisher
	{
		public int ID { get; set; }

		[MaxLength(50)]
		[Column("name")]
		public string Name { get; set; }
	}
}