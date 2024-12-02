using Entities.Models;

namespace Repositories.Contracts
{
  public interface IWorkerRepository : IRepositoryBase<Worker>
  {
    IQueryable<Worker> GetAllWorkers(bool trackChanges);
    Worker? GetWorker(int id, bool trackChanges);
    void AddWorker(Worker worker);
    void DeleteWorker(Worker worker);
    void UpdateWorker(Worker worker);

    IEnumerable<AvaliableTime> GetAvaliableTimesForWorker(int workerId, bool trackChanges);
    IEnumerable<Worker> GetWorkersByProfessionId(int professionId, bool trackChanges);
  }
}