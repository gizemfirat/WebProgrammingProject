namespace Entities.ViewModels
{
  public class AvaliableTimeDto
  {
    public int AvaliableTimeId { get; set; }
    public DateTime Time { get; set; }
    public String WorkerName { get; set; } 
    public String WorkerSurname { get; set; }
    public decimal Price { get; set; }
    public String ProcessName { get; set; }
  }
}