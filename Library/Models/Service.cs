using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Service
	{
		public int ID { get; set; }

		[Required]
		[MaxLength(15)]
        [Column("location")]
        public string Location { get; set; }

		[Required]
		[MaxLength(50)]
        [Column("name")]
		public string Name { get; set; }

		[Required]
		[Column("availability")]
		public bool Availability { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
    }
}