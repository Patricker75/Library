using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public partial class ResearchResource
    {
        public int ID { get; set; }

        [Column("website_name")]
        public string Name { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }

        public string Description { get; set; }
    }
}
