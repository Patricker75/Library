using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Relationships
{
    public class Manage
    {
        [ForeignKey("employee")]
        [Column("employee_id")]
        public int EmployeeID { get; set; }

        [ForeignKey("service")]
        [Column("service_id")]
        public int ServiceID { get; set; }
    }
}
