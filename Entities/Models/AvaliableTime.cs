namespace Entities.Models
{
  public class AvaliableTime
  {
    public int Id { get; set; }
    public int WorkerProcessId { get; set; }
    public DateTime Time { get; set; }
    public DateTime EndTime { get; set; }
    public int IsAvaliable { get; set; }
  }
}