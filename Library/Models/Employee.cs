using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("employee")]
    public partial class Employee
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

		[ForeignKey("login_user")]
		[Column("login_id")]
		public int LoginID { get; set; }

		[ForeignKey("employee")]
		[Column("supervisor_id")]
		public int? SupervisorID { get; set; }
	}
}
