using System.ComponentModel;
using System.Text.Json.Serialization;

namespace IMSystemUI.Domain
{
    public class ShelveType
    {
        [DisplayName("Shelf Id")]
        public Guid ShelfId { get; set; }

        //[JsonPropertyName("shelfTag")]
        [DisplayName("Shelf Tag")]
        public string ShelfTag { get; set; } = string.Empty;
    }
}
