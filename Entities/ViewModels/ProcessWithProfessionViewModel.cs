namespace Entities.ViewModels
{
  public class ProcessWithProfessionViewModel
  {
    public int Id { get; set; }
    public String Name { get; set; }
    public int Time { get; set; }
    public decimal Price { get; set; }
    public int ProfessionId { get; set; }
    public String ProfessionName { get; set; }
  }
}