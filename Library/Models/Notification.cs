using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Library.Models
{

    public partial class Notification
    {

        public int ID { get; set; }

        [ForeignKey("member")]
        [Column("member_id")]
        public int MemberID { get; set; }


        [Column("message")]
        public string Message { get; set; } = string.Empty;

        public bool Viewed { get; set; }



    }

}
