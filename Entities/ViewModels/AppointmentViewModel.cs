namespace Entities.ViewModels
{
  public class AppointmentViewModel
  {
    public int AppointmentId { get; set; }
    public String WorkerName { get; set; }
    public String ProfessionName { get; set; }
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
  }
}