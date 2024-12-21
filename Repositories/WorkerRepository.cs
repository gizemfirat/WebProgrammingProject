using Azure.Identity;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
  public class WorkerRepository : RepositoryBase<Worker>, IWorkerRepository
  {
    public WorkerRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<Worker> GetAllWorkers(bool trackChanges) => GetAll(trackChanges);

    public Worker? GetWorker(int id, bool trackChanges)
    {
      return Get(a => a.Id.Equals(id), trackChanges);
    }

    public void AddWorker(Worker worker)
    {
      Add(worker);
      _context.SaveChanges();
    }

    public void DeleteWorker(Worker worker)
    {
      Delete(worker);
      _context.SaveChanges();
    }

    public void UpdateWorker(Worker worker)
    {
      Update(worker);
      _context.SaveChanges();
    }

    public IEnumerable<Worker> GetWorkersByProcessId(int processId)
    {
      return _context.WorkerProcesses
      .Where(wp => wp.ProcessId == processId)
      .Select(wp => _context.Workers.FirstOrDefault(w => w.Id == wp.WorkerId))
      .ToList();
    }

    public async Task<IEnumerable<WorkerViewModel>> GetWorkersWithProcessesAsync()
    {
      var workers = await _context.Workers
                    .Select(worker => new WorkerViewModel
                    {
                      Id = worker.Id,
                      Name = worker.Name,
                      Surname = worker.Surname,
                      Salary = worker.Salary,
                      Processes = _context.WorkerProcesses
                                  .Where(wp => wp.WorkerId == worker.Id)
                                  .Join(
                                    _context.Processes,
                                    wp => wp.ProcessId,
                                    p => p.Id,
                                    (wp, p) => new ProcessDto
                                    {
                                      Id = p.Id,
                                      Name = p.Name
                                    })
                                    .ToList()
                    }).ToListAsync();

      return workers;
    }

    public async Task AddWorkerAsync(Worker worker, List<int> processIds)
    {
      await _context.Workers.AddAsync(worker);
      await _context.SaveChangesAsync();

      foreach (var processId in processIds)
      {
        var workerProcess = new WorkerProcess
        {
          WorkerId = worker.Id,
          ProcessId = processId
        };

        await _context.WorkerProcesses.AddAsync(workerProcess);
      }

      await _context.SaveChangesAsync();
    }

    public bool IsProcessInAppointment(int workerId, int processId)
    {
      var query = from appointment in _context.Appointments
                  join avaliableTime in _context.AvaliableTimes
                    on appointment.AvaliableTimeId equals avaliableTime.Id
                  join workerProcess in _context.WorkerProcesses
                    on avaliableTime.WorkerProcessId equals workerProcess.Id
                  where workerProcess.WorkerId == workerId && workerProcess.ProcessId == processId
                  select appointment;
      return query.Any();
    }

        public void UpdateWorkerProcesses(int workerId, List<int> newProcessIds)
        {
          var currentProcesses = _context.WorkerProcesses
                                         .Where(wp => wp.WorkerId == workerId)
                                         .ToList();

          var processesToDelete = currentProcesses.Where(wp => !newProcessIds.Contains(wp.ProcessId)).ToList();
          _context.WorkerProcesses.RemoveRange(processesToDelete);

          var processesToAdd = newProcessIds.Where(id => !currentProcesses.Any(wp => wp.ProcessId == id))
                                            .Select(id => new WorkerProcess {WorkerId = workerId, ProcessId = id})
                                            .ToList();
          
          _context.WorkerProcesses.AddRange(processesToAdd);
          _context.SaveChanges();
        }

        public bool HasAppointment(int workerId)
        {
          var workerProcessIds = _context.WorkerProcesses
                                         .Where(wp => wp.WorkerId == workerId)
                                         .Select(wp => wp.Id)
                                         .ToList();
                                    
          var avaliableTimeIds = _context.AvaliableTimes
                                         .Where(at => workerProcessIds.Contains(at.WorkerProcessId))
                                         .Select(at => at.Id)
                                         .ToList();

          return _context.Appointments
                         .Any(a => avaliableTimeIds.Contains(a.AvaliableTimeId));
        }

        public void DeleteWorker(int workerId)
        {
          var workerProcesses = _context.WorkerProcesses
                                        .Where(wp => wp.WorkerId == workerId)
                                        .ToList();

          _context.WorkerProcesses.RemoveRange(workerProcesses);

          var avaliableTimes = _context.AvaliableTimes
                                       .Where(at => workerProcesses.Select(wp => wp.Id).Contains(at.WorkerProcessId))
                                       .ToList();

          _context.AvaliableTimes.RemoveRange(avaliableTimes);

          var worker = _context.Workers.FirstOrDefault(w => w.Id == workerId);
          if(worker != null) {
            _context.Workers.Remove(worker);
          }

          _context.SaveChanges();
        }
    }
}