using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{

	public partial class Member
	{
		public int ID { get; set; }

		[Required]
		[MaxLength(20)]
        [Column("username")]
        public string Username { get; set; } = string.Empty;

		[Required]
		[MaxLength(10)]
		[DataType(DataType.Password)]
		[Column("password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        [Column("first_name")]
		public string FirstName { get; set; } = string.Empty;

		[MaxLength(1)]
        [Column("middle_initial")]
		public string? MiddleName { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("last_name")]
		public string LastName { get; set; } = string.Empty;

		[MaxLength(10)]
		[DataType(DataType.PhoneNumber)]
        [Column("phone_number")]
		public string? PhoneNumber { get; set; }

		[Required]
		[MaxLength(150)]
		public string? Address { get; set; }
		
		public Gender Gender { get; set; }

		[DataType(DataType.Date)]
		[Column("birth_date")]
		public DateTime BirthDate { get; set; }

		[DataType(DataType.Date)]
		[Column("join_date")]
		public DateTime JoinDate { get; set; }

		[Column("member_type")]
		public MemberType MemberType { get; set; }

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
