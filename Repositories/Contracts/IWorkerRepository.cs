using Entities.Models;
using Entities.ViewModels;

namespace Repositories.Contracts
{
  public interface IWorkerRepository : IRepositoryBase<Worker>
  {
    IQueryable<Worker> GetAllWorkers(bool trackChanges);
    Worker? GetWorker(int id, bool trackChanges);
    void AddWorker(Worker worker);
    void DeleteWorker(Worker worker);
    void UpdateWorker(Worker worker);
    IEnumerable<Worker> GetWorkersByProcessId(int processId);
    Task<IEnumerable<WorkerViewModel>> GetWorkersWithProcessesAsync();
    Task AddWorkerAsync(Worker worker, List<int> processIds);
  }
}