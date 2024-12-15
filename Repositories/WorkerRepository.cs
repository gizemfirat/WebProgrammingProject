using Entities.Models;
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
  }
}