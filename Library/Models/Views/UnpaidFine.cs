using Library.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Views
{
    public class UnpaidFine
    {
        [Column("member_name")]
        public string MemberName { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        [Column("fine_date")]
        public DateTime FineDate { get; set; }

        [Column("item_type")]
        public ItemType ItemType { get; set; }

        [Column("item_name")]
        public string ItemName { get; set; }
    }
}
