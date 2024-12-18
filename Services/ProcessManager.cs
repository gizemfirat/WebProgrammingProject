using Services.Contracts;
using Entities.Models;
using Repositories.Contracts;
using Entities.ViewModels;

namespace Services
{
  public class ProcessManager : IProcessService
  {
    private readonly IRepositoryManager _manager;

    public ProcessManager(IRepositoryManager manager)
    {
      _manager = manager;
    }
    public IEnumerable<Process> GetProcesses(bool trackChanges)
    {
      return _manager.Process.GetAllProcesses(trackChanges);
    }
    public Process? GetProcess(int id, bool trackChanges)
    {
      var Process = _manager.Process.GetProcess(id, trackChanges);
      if (Process is null)
      {
        throw new Exception("Process not found");
      }

      return Process;
    }

    public void AddProcess(Process process)
    {
      _manager.Process.AddProcess(process);
    }

    public void DeleteProcess(Process process)
    {
      _manager.Process.DeleteProcess(process);
    }

    public void UpdateProcess(Process process)
    {
      _manager.Process.UpdateProcess(process);
    }
    public IEnumerable<Process> GetProcessesByProfession(int professionId)
    {
      return _manager.Process.GetProcessesByProfessionId(professionId);
    }

    public IEnumerable<ProcessGroupDto> GetGroupedProcesses()
    {
      return _manager.Process.GetProcessesGroupedByProfession();
    }

    public IEnumerable<ProcessWithProfessionViewModel> GetProcessesWithProfession()
    {
      return _manager.Process.GetProcessesWithProfession();
    }
  }
}