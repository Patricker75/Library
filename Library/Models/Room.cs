using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Room
	{
		public int ID { get; set; }

		public string Location { get; set; }

		[Column("availability")]
		public bool IsAvailable { get; set; }
		
		[Column("room_type")]
		private short roomType { get; set; }
		[NotMapped]
		public RoomType RoomType
		{
			get
			{
				return (RoomType)roomType;
			}
			set
			{
				roomType = (short)value;
			}
		} 

		[ForeignKey("Member")]
		[Column("reserver_id")]
		public int? ReserverID { get; set; }
	}
}
