namespace Entities.ViewModels
{
  public class WorkerViewModel
  {
    public int Id { get; set; }
    public String Name { get; set; }
    public String Surname { get; set; }
    public decimal Salary { get; set; }
    public List<ProcessDto> Processes { get; set; }
  }
}