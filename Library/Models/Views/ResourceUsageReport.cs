using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Views
{
    public class ResourceUsageReport
    {
        [DataType(DataType.Date)]
        [Column("time_stamp")]
        public DateTime TimeStamp { get; set; }

        public string Name { get; set; }
    }
}
