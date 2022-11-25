using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Views
{
    public class MemberReport
    {
        [Column("checked_out")]
        public int CheckedOut { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Column("join_date")]
        public DateTime JoinDate { get; set; }

        

        
    }
}
