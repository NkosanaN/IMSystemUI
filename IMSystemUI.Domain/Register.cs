using System.ComponentModel.DataAnnotations;

namespace IMSystemUI.Domain;

public class Register
{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Name")]
    public string FirstName { get; set; }

    [Display(Name = "Surname")]
    public string LastName { get; set; }
}