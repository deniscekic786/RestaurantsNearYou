using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebScraper.Models
{
    [Table("Restaurant")]
    public partial class Restaurant
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Category { get; set; }

        [StringLength(1000)]
        public string ImageReference { get; set; }

        [StringLength(1000)]
        public string ImageSource { get; set; }

        public int AddressId { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public virtual Address Address { get; set; }
    }
}
