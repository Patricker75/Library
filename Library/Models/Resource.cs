using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("resource")]
    public class Resource
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.Url)]
        public string Url { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Column("date_added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
    }
}
