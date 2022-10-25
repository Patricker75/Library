using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Notification
    {
        public int ID { get; set; }

        public string Message { get; set; }

        public bool Viewed { get; set; }

        [ForeignKey("member")]
        [Column("member_id")]
        public int MemberID { get; set; }
    }
}
