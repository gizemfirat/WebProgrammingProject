using Entities.Models;
using Entities.ViewModels;
using Repositories.Contracts;

namespace Repositories
{
  public class ProcessRepository : RepositoryBase<Process>, IProcessRepository
  {
    public ProcessRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<Process> GetAllProcesses(bool trackChanges) => GetAll(trackChanges);

    public Process? GetProcess(int id, bool trackChanges)
    {
      return Get(a => a.Id.Equals(id), trackChanges);
    }

    public void AddProcess(Process process)
    {
      Add(process);
      _context.SaveChanges();
    }

    public void Delete(Process process)
    {
      Delete(process);
      _context.SaveChanges();
    }

    public void UpdateProcess(Process process)
    {
      Update(process);
      _context.SaveChanges();
    }

    public IEnumerable<Process> GetProcessesByProfessionId(int professionId)
    {
      return _context.Processes
      .Where(p => p.ProfessionId == professionId)
      .ToList();
    }

    public IEnumerable<ProcessGroupDto> GetProcessesGroupedByProfession()
    {
      var result = _context.Processes
                   .Join(_context.Professions,
                         process => process.ProfessionId,
                         profession => profession.Id,
                         (process, profession) => new
                         {
                           process,
                           profession.Name
                         })
                   .GroupBy(p => new { p.process.ProfessionId, p.Name })
                   .Select(g => new ProcessGroupDto
                   {
                     ProfessionId = g.Key.ProfessionId,
                     ProfessionName = g.Key.Name,
                     Processes = g.Select(x => x.process).ToList()
                   })
                   .ToList();
      return result;
    }

    public IEnumerable<ProcessWithProfessionViewModel> GetProcessesWithProfession()
    {
      var result = _context.Processes
                  .Join(_context.Professions,
                        process => process.ProfessionId,
                        profession => profession.Id,
                        (process, profession) => new ProcessWithProfessionViewModel
                        {
                          Id = process.Id,
                          Name = process.Name,
                          Price = process.Price,
                          Time = process.Time,
                          ProfessionId = profession.Id,
                          ProfessionName = profession.Name
                        }).ToList();

      return result;
    }

    public bool HasAppointment(int processId)
    {
      return (from appointment in _context.Appointments
              join avaliableTime in _context.AvaliableTimes on appointment.AvaliableTimeId equals avaliableTime.Id
              join workerProcess in _context.WorkerProcesses on avaliableTime.WorkerProcessId equals workerProcess.Id
              where workerProcess.ProcessId == processId
              select appointment).Any();
    }

    public void DeleteProcess(int processId)
    {
      var workerProcesses = _context.WorkerProcesses
      .Where(wp => wp.ProcessId == processId).ToList();

      foreach(var workerProcess in workerProcesses) {
        var avaliableTimes = _context.AvaliableTimes
        .Where(at => at.WorkerProcessId == workerProcess.Id).ToList();

        _context.AvaliableTimes.RemoveRange(avaliableTimes);
      }

      _context.WorkerProcesses.RemoveRange(workerProcesses);

      var process = _context.Processes.Find(processId);
      if(process != null) {
        _context.Processes.Remove(process);
      }

      _context.SaveChanges();
    }
  }
}