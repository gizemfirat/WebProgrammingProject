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

          foreach(var processId in processIds) {
            var workerProcess = new WorkerProcess
            {
              WorkerId = worker.Id,
              ProcessId = processId
            };

            await _context.WorkerProcesses.AddAsync(workerProcess);
          }

          await _context.SaveChangesAsync();
        }
    }
}