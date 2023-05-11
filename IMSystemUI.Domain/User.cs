using System.ComponentModel;

namespace IMSystemUI.Domain;
public class User
{
    public Guid Id { get; set; }

    public string DisplayName { get; set; } = string.Empty;
    public string? AddressLine1 { get; set; } = string.Empty;
    public string? AddressLine2 { get; set; }
    public virtual string? UserName { get; set; }
    public virtual string? Email { get; set; }
}
