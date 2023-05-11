using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IMSystemUI.Domain;

public class Supplier
{

    [DisplayName("Supplier Id")]
    public Guid SupplierId { get; set; }

    [DisplayName("Supplier Name")]
    public string SupplierName { get; set; } = string.Empty;

    [DisplayName("Description")]
    public string SupplierDescription { get; set; } = string.Empty;
    [DisplayName("Booking Date")]
    public DateTime BookingDate { get; set; } = DateTime.Now;
}
