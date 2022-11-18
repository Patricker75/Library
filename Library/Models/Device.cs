using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Device
	{
		public int ID { get; set; }

<<<<<<< HEAD
		[Column("item_type")]
		public ItemType ItemType { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[Column("condition")]
		public Condition Condition { get; set; }
	}
=======
		[Required]
		public DeviceType Type { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
    }
>>>>>>> model
}