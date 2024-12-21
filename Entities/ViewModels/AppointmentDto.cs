namespace Entities.ViewModels
{
  public class AppointmentDto
  {
    public int AppointmentId { get; set; }
    public String ProcessName { get; set; }
    public String WorkerFullName { get; set; }
    public int WorkerProcessId { get; set; }
    public int AvaliableTimeId { get; set; }
    public int IsAvaliable { get; set; }
    public DateTime Date { get; set; }
    public DateTime EndTime { get; set; }
    public int EstablishedTime { get; set; }
    public decimal Price { get; set; }
    public int CustomerId { get; set; }
    public String CustomerFullName { get; set; }
  }
}