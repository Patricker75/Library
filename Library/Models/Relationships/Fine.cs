using Library.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Relationships
{
    [Table("fine")]
    public class Fine
    {
        public int ID { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("fine_date")]
        public DateTime FineDate { get; set; }

        [DataType(DataType.Date)]
        [Column("paid_date")]
        public DateTime? PaidDate { get; set; }

        [ForeignKey("member")]
        [Column("member_id")]
        public int MemberID { get; set; }

        [ForeignKey("checkout")]
        [Column("checkout_id")]
        public int CheckoutID { get; set; }
    }
}
