namespace Entities.Models
{
  public class Process
  {
    public int Id { get; set; }
    public int ProfessionId { get; set; }
    public String Name { get; set; }
    public int Time { get; set; }
    public decimal Price { get; set; }
  }
}