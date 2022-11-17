using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public partial class Employee
	{
		public int ID { get; set; }

		[Required]
		[MaxLength(20)]
		[Column("username")]
		public string Username { get; set; }

        [Required]
		[DataType(DataType.Password)]
        [MaxLength(10)]
        [Column("password")]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("first_name")]
		public string FirstName { get; set; }

        [MaxLength(1)]
        [Column("middle_initial")]
		public string? MiddleName { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("last_name")]
		public string LastName { get; set; }

        [Required]
		[DataType(DataType.PhoneNumber)]
        [MaxLength(10)]
        [Column("phone_number")]
		public string? PhoneNumber { get; set; }

        [Required]
        [MaxLength(150)]
        public string? Address { get; set; }

		public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
		[Column("birth_date")]
		public DateTime BirthDate { get; set; }

        [Required]
		[DataType(DataType.Date)]
		[Column("hire_date")]
		public DateTime HireDate { get; set; }

		[Column("hours_worked")]
		public decimal HoursWorked { get; set; }

        [Required]
		[Column("job_title")]
		public string JobTitle { get; set; }

        [Required]
        [DataType(DataType.Currency)]
		public decimal Salary { get; set; }

		[ForeignKey("Employee")]
		[Column("supervisor_id")]
		public int? SupervisorID { get; set; } 
    }
}
