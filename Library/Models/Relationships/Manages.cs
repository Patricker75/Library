using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Relationships
{
    public class Manages
    {
        [ForeignKey("Employee")]
        [Column("employee_id")]
        public int EmployeeID { get; set; }

        [ForeignKey("Service")]
        [Column("service_id")]
        public int ServiceID { get; set; }
    }
}
