using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Room
	{
		public int ID { get; set; }

		[Required]
		[MaxLength(10)]
        [Column("location")]
        public string Location { get; set; }

		[Column("availability")]
		public bool IsAvailable { get; set; }

		[Required]
		[Column("room_type")]
		public RoomType RoomType { get; set; }

		[ForeignKey("Member")]
		[Column("reserver_id")]
		public int? ReserverID { get; set; }
	}
}
