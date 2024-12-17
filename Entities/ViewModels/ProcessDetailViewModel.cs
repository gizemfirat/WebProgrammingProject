namespace Entities.ViewModels
{
  public class ProcessDetailViewModel
  {
    public int AvaliableTimeId { get; set; }
    public String ProcessName { get; set; }
    public String WorkerName { get; set; }
    public DateTime AvaliableTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal Price { get; set; }
  }
}