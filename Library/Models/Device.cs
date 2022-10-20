using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public partial class Device
    {
        public int ID { get; set; }


        [Column("item_type")]
        public int ItemType { get; set; }

        [Column("name")]
        public char Name { get; set; }

        [Column("condition")]
        public int Condition { get; set; }
    }
}