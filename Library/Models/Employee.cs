using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Employee
	{
		public int ID { get; set; }

<<<<<<< HEAD
		[Column("username")]
		public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("first_name")]
=======
		[Required]
		[MaxLength(20)]
		[Column("first_name")]
>>>>>>> model
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

<<<<<<< HEAD
		public string? Address { get; set; }

=======
		[Required]
>>>>>>> model
		public Gender Gender { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[Column("birth_date")]
		public DateTime BirthDate { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[Column("hire_date")]
		public DateTime HireDate { get; set; }

		[Required]
		[Column("job_role")]
		public JobRole JobRole { get; set; }

		[Required]
		public decimal Salary { get; set; }

		[Required]
		[Column("hours_worked")]
		public decimal HoursWorked { get; set; }

		[ForeignKey("loginuser")]
		[Column("login_id")]
		public int LoginID { get; set; }

		[ForeignKey("employee")]
		[Column("supervisor_id")]
		public int? SupervisorID { get; set; }


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
