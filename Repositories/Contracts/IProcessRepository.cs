using Entities.Models;
using Entities.ViewModels;

namespace Repositories.Contracts
{
  public interface IProcessRepository : IRepositoryBase<Process>
  {
    IQueryable<Process> GetAllProcesses(bool trackChanges);
    Process? GetProcess(int id, bool trackChanges);
    void AddProcess(Process process);
    void Delete(Process process);
    void UpdateProcess(Process process);
    IEnumerable<Process> GetProcessesByProfessionId(int professionId);
    IEnumerable<ProcessGroupDto> GetProcessesGroupedByProfession();
    IEnumerable<ProcessWithProfessionViewModel> GetProcessesWithProfession();
    bool HasAppointment(int processId);
    void DeleteProcess(int processId);

  }
}