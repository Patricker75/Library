using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("memberreport")]
    public partial class Report
    {

        public int ID { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("join_date")]
        public DateTime JoinDate { get; set; }

        [Column("check_out_date")]
        public DateTime CheckOutDate { get; set; }

        [Column("returned")]
        public bool Returned { get; set; }


    }
}
