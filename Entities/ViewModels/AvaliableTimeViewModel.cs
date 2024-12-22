namespace Entities.ViewModels
{
  public class AvaliableTimeViewModel
  {
    public int AvaliableTimeId { get; set; }
    public int WorkerProcessId { get; set; }
    public int WorkerId { get; set; }
    public int ProcessId { get; set; }
    public String WorkerName { get; set; }
    public String WorkerSurname { get; set; }
    public String ProcessName { get; set; }
    public DateTime Time { get; set; }
    public DateTime EndTime { get; set; }
    public int IsAvaliable { get; set; }
  }
}