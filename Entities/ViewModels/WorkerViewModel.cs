namespace Entities.ViewModels
{
  public class WorkerViewModel
  {
    public String Name { get; set; }
    public String Surname { get; set; }
    public decimal Salary { get; set; }
    public List<ProcessDto> Processes { get; set; }
  }
}