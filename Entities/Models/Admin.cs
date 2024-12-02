using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
  public class Admin
  {
    public int Id { get; set; }
    [Required(ErrorMessage = "Username is required.")]
    public String Username { get; set; }
    [Required(ErrorMessage = "Password is required.")]
    public String Password { get; set; }
  }
}