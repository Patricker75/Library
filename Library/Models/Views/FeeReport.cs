using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models.Views
{
    public class FeeReport
    {
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [Column("check_out_date")]
        public DateTime CheckoutDate { get; set; }

        [DataType(DataType.Date)]
        [Column("due_date")]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Date)]
        [Column("return_date")]
        public DateTime? ReturnDate { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        [Column("paid_date")]
        public DateTime? PaidDate { get; set; }
    }
}
