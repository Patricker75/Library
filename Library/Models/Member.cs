using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{

	public partial class Member
	{
		public int ID { get; set; }

        [Column("username")]
        public string Username { get; set; } = string.Empty;
        
		[Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("first_name")]
		public string FirstName { get; set; } = string.Empty;

        [Column("middle_initial")]
		public string? MiddleName { get; set; }

		[Column("last_name")]
		public string LastName { get; set; } = string.Empty;

        [Column("phone_number")]
		public string? PhoneNum { get; set; }

		public string? Address { get; set; }
		
		public Gender Gender { get; set; }

		[DataType(DataType.Date)]
		[Column("birth_date")]
		public DateTime BirthDate { get; set; }

		[DataType(DataType.Date)]
		[Column("join_date")]
		public DateTime JoinDate { get; set; } = DateTime.Now;

		[Column("member_type")]
		public MemberType MemberType { get; set; }

		// Could Drop
		[Column("check_out_count")]
		public short CheckOutCount { get; set; }

		[Column("check_out_limit")]
		public short CheckOutLimit { get; set; }

		[Column("status")]
		public MemberStatus MemberStatus { get; set; }

		[DataType(DataType.Currency)]
		public decimal Balance { get; set; }

        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName));
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName);
        }
    }
}
