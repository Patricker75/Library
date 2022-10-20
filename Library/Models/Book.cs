using Library.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public partial class Book
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;

        [Column("dewey_num")]
        public string DeweyNum { get; set; } = string.Empty;
        
        public Condition Condition { get; set; }
    }
}
