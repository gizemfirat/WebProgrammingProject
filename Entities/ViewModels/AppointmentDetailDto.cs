namespace Entities.ViewModels
{
  public class AppointmentDetailDto
  {
    public int AppointmentId { get; set; }
    public String ProcessName { get; set; }
    public String WorkerFullName { get; set; }
    public DateTime Date { get; set; }
    public int EstablishedTime { get; set; }
    public decimal Price { get; set; }
  }
}