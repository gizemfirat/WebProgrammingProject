namespace Entities.Models
{
  public class AvaliableTime
  {
    public int Id { get; set; }
    public int Worker { get; set; }
    public DateTime Time { get; set; }
    public int IsAvaliable { get; set; }
  }
}