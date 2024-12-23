namespace Entities.ViewModels
{
  public class AppointmentForWorkerDto
  {
    public int AppointmentId { get; set; }
    public String ProcessName { get; set; }
    public int WorkerProcessId { get; set; }
    public int AvaliableTimeId { get; set; }
    public int IsApproved { get; set; }
    public DateTime Time { get; set; }
    public DateTime EndTime { get; set; }
    public String CustomerFullName { get; set; }
  }
}