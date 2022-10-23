using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{

	public partial class Member
	{
		public int ID { get; set; }

		public string Username { get; set; }

		[Column("first_name")]
		public string FirstName { get; set; }

		[Column("middle_initial")]
		public string? MiddleName { get; set; }

		[Column("last_name")]
		public string LastName { get; set; }

		[Column("phone_number")]
		public string? PhoneNum { get; set; }

		public string? Address { get; set; }
		
		private short gender { get; set; }
		[NotMapped]
		public Gender Gender
		{
			get
			{
				return (Gender)gender;
			}
			set
			{
				gender = (short)value;
			}
		}

		[DataType(DataType.Date)]
		[Column("birth_date")]
		public DateTime BirthDate { get; set; }

		[DataType(DataType.Date)]
		[Column("join_date")]
		public DateTime JoinDate { get; set; } = DateTime.Now;

		[Column("member_type")]
		private short memberType { get; set; }
		[NotMapped]
		public MemberType MemberType
		{
			get
			{
				return (MemberType)memberType;
			}
			set
			{
				memberType = (short)value;
			}
		}

		// Could Drop
		[Column("check_out_count")]
		public short CheckOutCount { get; set; }

		[Column("check_out_limit")]
		public short CheckOutLimit { get; set; }

		[Column("status")]
		private short memberStatus { get; set; }
		[NotMapped]
		public MemberStatus MemberStatus
		{
			get
			{
				return (MemberStatus)memberStatus;
			}
			set
			{
				memberStatus = (short)value;
			}
		}

		[DataType(DataType.Currency)]
		public decimal Balance { get; set; }
	}
}
