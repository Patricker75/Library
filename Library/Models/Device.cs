using Library.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Device
	{
		public int ID { get; set; }


		[Column("item_type")]
		private short itemType { get; set; }
		[NotMapped]
		public ItemType ItemType
		{
			get
			{
				return (ItemType)itemType;
			}
			set
			{
				itemType = (short)value;
			}
		}

		[Column("name")]
		public string Name { get; set; }

		[Column("condition")]
		private short condition { get; set; }
		[NotMapped]
		public Condition Condition
		{
			get
			{
				return (Condition)condition;
			}
			set
			{
				condition = (short)value;
			}
		}
	}
}