using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Views
{
    public class ServiceUsageReport
    {
        [DataType(DataType.Date)]
        [Column("time_stamp")]
        public DateTime TimeStamp { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }
    }
}
