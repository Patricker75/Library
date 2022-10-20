using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public partial class Book
    {
        public int ID { get; set; }


        [Column("title")]
        public string Title { get; set; }

        [Column("genre")]
        public string Genre { get; set; }

        [Column("summary")]
        public string Summary { get; set; }

        [Column("dewey_num")]
        public string DeweyNumber { get; set; }

        [Column("condition")]
        public int Condition { get; set; }

        [Column("audience")]
        public int Audience { get; set; }
    }
}