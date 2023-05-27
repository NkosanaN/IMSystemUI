using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IMSystemUI.Domain
{
    public class Item
    {
        [DisplayName("Item Id")]
        public Guid ItemId { get; set; }

        [DisplayName("Serial No")]
        public string Serialno { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        [DisplayName("Item Name")]
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;

        [DisplayName("Item Tag")]
        public string? ItemTag { get; set; }
        public float Cost { get; set; } = 0.00f;//should be uniqueCost
        public ShelveType? ShelveBy { get; set; }

        [DisplayName("Quantity")]
        public float Qty { get; set; } = 0.00f;
        
        [DisplayName("Date Purchased"), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DatePurchased { get; set; }

        [DisplayName("is Due for Repair")]
        public bool DueforRepair { get; set; }

        public string CreatedById { get; set; }
        public User? CreatedBy { get; set; }

    }
}
