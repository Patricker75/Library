using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Relationships
{
    public class Borrows
    {
        private bool Returned { get; set; }

        [DataType(DataType.Date)]
        [Column("return_date")]
        public DateTime ReturnDate { get; set; }

        [ForeignKey("Member")]
        [Column("member_id")]
        public int MemberID { get; set; }

        [ForeignKey("Device")]
        [Column("device_id")]
        public int DeviceID { get; set; }
    }
}
