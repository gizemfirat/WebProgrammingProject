using Entities.Models;

namespace Services.Contracts
{
  public interface IProcessService
  {
    IEnumerable<Process> GetProcesses(bool trackChanges);
    Process? GetProcess(int id, bool trackChanges);
    void AddProcess(Process process);
    void DeleteProcess(Process process);
    void UpdateProcess(Process process);
    IEnumerable<Process> GetProcessesByProfession(int professionId);
  }
}