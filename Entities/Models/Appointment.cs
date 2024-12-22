namespace Entities.Models
{
  public class Appointment
  {
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int AvaliableTimeId { get; set; }
    public int IsApproved { get; set; } = 0;
  }
}