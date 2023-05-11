using System.ComponentModel;

namespace IMSystemUI.Domain
{
    public class ShelveType
    {
        [DisplayName("Shelf Id")]
        public Guid ShelfId { get; set; }
        [DisplayName("Shelf Tag")]
        public string ShelfTag { get; set; } = string.Empty;
    }
}
