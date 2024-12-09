using Entities.Models;

namespace Repositories.Contracts
{
  public interface IWorkerProcessRepository : IRepositoryBase<WorkerProcess>
  {
    IQueryable<WorkerProcess> GetAllWorkerProcesses(bool trackChanges);
    WorkerProcess? GetWorkerProcess(int id, bool trackChanges);
    void AddWorkerProcess(WorkerProcess workerProcess);
    void DeleteWorkerProcess(WorkerProcess workerProcess);
    void UpdateWorkerProcess(WorkerProcess workerProcess);
  }
}