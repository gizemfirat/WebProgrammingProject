using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
  public class WorkerProcessRepository : RepositoryBase<WorkerProcess>, IWorkerProcessRepository
  {
    public WorkerProcessRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<WorkerProcess> GetAllWorkerProcesses(bool trackChanges) => GetAll(trackChanges);

    public WorkerProcess? GetWorkerProcess(int id, bool trackChanges)
    {
      return Get(a => a.Id.Equals(id), trackChanges);
    }

    public void AddWorkerProcess(WorkerProcess workerProcess)
    {
      Add(workerProcess);
      _context.SaveChanges();
    }

    public void DeleteWorkerProcess(WorkerProcess workerProcess)
    {
      Delete(workerProcess);
      _context.SaveChanges();
    }

    public void UpdateWorkerProcess(WorkerProcess workerProcess)
    {
      Update(workerProcess);
      _context.SaveChanges();
    }
  }
}