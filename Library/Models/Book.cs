using Library.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Book
	{
		public int ID { get; set; }


		[Column("title")]
		public string Title { get; set; }

		[Column("genre")]
		public string Genre { get; set; }

		[Column("summary")]
		public string Summary { get; set; }

		[Column("dewey_num")]
		public string DeweyNumber { get; set; }

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

		[Column("audience")]
		private short audience { get; set; }
		[NotMapped]
		public Audience Audience
		{
			get
			{
				return (Audience)audience;
			}
			set
			{
				audience = (short)value;
			}
		}
	}
}