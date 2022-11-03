using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Employee
	{
		public int ID { get; set; }

		[Column("username")]
		public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("first_name")]
		public string FirstName { get; set; }

		[Column("middle_initial")]
		public string? MiddleName { get; set; }

		[Column("last_name")]
		public string LastName { get; set; }

		[Column("phone_number")]
		public string? PhoneNum { get; set; }

		public string? Address { get; set; }

		public Gender Gender { get; set; }

		[DataType(DataType.Date)]
		[Column("birth_date")]
		public DateTime BirthDate { get; set; }

		[DataType(DataType.Date)]
		[Column("hire_date")]
		public DateTime HireDate { get; set; }

		[Column("hours_worked")]
		public decimal HoursWorked { get; set; }

		[Column("job_title")]
		public string JobTitle { get; set; }

		public decimal Salary { get; set; }

		[ForeignKey("Employee")]
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
