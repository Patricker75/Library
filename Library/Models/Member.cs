using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("member")]
    public partial class Member
	{
		public int ID { get; set; }

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

        [Required]
        [DataType(DataType.Date)]
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Date)]
		[Column("join_date")]
		public DateTime JoinDate { get; set; }

        [Required]
        public MemberType Type { get; set; }

        [Required]
        public MemberStatus Status { get; set; }

        [Required]
		[Column("check_out_limit")]
		public int CheckOutLimit { get; set; }

        [Required]
		[DataType(DataType.Currency)]
        [Column("balance")]
		public decimal Balance { get; set; }

        [Required]
        [ForeignKey("loginuser")]
        [Column("login_id")]
        public int LoginID { get; set; }
	}
}
