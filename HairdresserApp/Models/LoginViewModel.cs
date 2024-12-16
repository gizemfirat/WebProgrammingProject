using System.ComponentModel.DataAnnotations;

namespace HairdresserApp.Models
{
  public class LoginViewModel
  {
    [Required]
    [EmailAddress]
    public String Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public String Password { get; set; }
    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
  }
}