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

    IEnumerable<Worker> GetWorkersByProfessionId(int professionId, bool trackChanges);
  }
}