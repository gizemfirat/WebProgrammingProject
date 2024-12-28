using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
  public class RegisterViewModel
  {
    [Required]
    public String Name { get; set; }
    [Required]
    public String Surname { get; set; }
    [Required]
    [EmailAddress]
    public String Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public String Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public String ConfirmPassword { get; set; }
  }
}