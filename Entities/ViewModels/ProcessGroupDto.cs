using Entities.Models;

namespace Entities.ViewModels
{
  public class ProcessGroupDto
  {
    public int ProfessionId { get; set; }
    public String ProfessionName { get; set; }
    public List<Process> Processes { get; set; }
  }
}