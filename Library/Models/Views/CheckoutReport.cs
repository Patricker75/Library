using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Views
{
    public class CheckoutReport
    {
        [Column("member_id")]
        public int MemberID { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Column("check_out_date")]
        public DateTime CheckoutDate { get; set; }

        [DataType(DataType.Date)]
        [Column("return_date")]
        public DateTime? ReturnDate { get; set; }

        [Column("item_type")]
        public ItemType ItemType { get; set; }

        [Column("item_name")]
        public string ItemName { get; set; }
    }
}
