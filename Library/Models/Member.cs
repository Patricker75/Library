using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{

	public partial class Member
	{
		public int ID { get; set; }

<<<<<<< HEAD
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
=======
        [Required]
        [MaxLength(20)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [MaxLength(1)]
        [Column("middle_name")]
        public string? MiddleName { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("last_name")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Address { get; set; }

        [Required]
        [MinLength(10), MaxLength(10)]
        [RegularExpression(@"^[0-9]*$")]
        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Required]
        public Gender Gender { get; set; }
>>>>>>> model

        [Required]
        [DataType(DataType.Date)]
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Date)]
		[Column("join_date")]
		public DateTime JoinDate { get; set; }

<<<<<<< HEAD
		[Column("member_type")]
		public MemberType MemberType { get; set; }
=======
        [Required]
        public MemberType Type { get; set; }
>>>>>>> model

        [Required]
        public MemberStatus Status { get; set; }

        [Required]
		[Column("check_out_limit")]
<<<<<<< HEAD
		public short CheckOutLimit { get; set; }

		[Column("status")]
		public MemberStatus MemberStatus { get; set; }
=======
		public int CheckOutLimit { get; set; }
>>>>>>> model

        [Required]
		[DataType(DataType.Currency)]
<<<<<<< HEAD
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
=======
        [Column("amount_owed")]
		public decimal AmountOwed { get; set; }

        [Required]
        [ForeignKey("loginuser")]
        [Column("login_id")]
        public int LoginID { get; set; }
	}
>>>>>>> model
}
