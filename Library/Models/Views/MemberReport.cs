using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Views
{
    public class MemberReport
    {
        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Column("join_date")]
        public DateTime JoinDate { get; set; }

        [DataType(DataType.Date)]
        [Column("check_out_date")]
        public DateTime? CheckOutDate { get; set; }

        [Column("returned")]
        public bool? IsReturned { get; set; }
    }
}
