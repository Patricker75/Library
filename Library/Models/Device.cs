using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("device")]
    public partial class Device
	{
		public int ID { get; set; }

		[Required]
		public DeviceType Type { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

        [Column("date_added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
    }
}