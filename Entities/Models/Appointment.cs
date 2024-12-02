namespace Entities.Models
{
  public class Appointment
  {
    public int Id { get; set; }
    public int Customer { get; set; }
    public int Worker { get; set; }
    public int Date { get; set; }
  }
}