using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("room")]
    public partial class Room
	{
		public int ID { get; set; }

		[Required]
		[MaxLength(15)]
		public string Location { get; set; }

		[Required]
		public RoomType Type { get; set; }

		[Required]
		[Column("available")]
		public bool IsAvailable { get; set; }

        [Column("date_added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }

        [ForeignKey("Member")]
		[Column("member_id")]
		public int? MemberID { get; set; }
	}
}
