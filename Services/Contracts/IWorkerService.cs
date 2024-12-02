using Entities.Models;
using Microsoft.Identity.Client;

namespace Services.Contracts
{
  public interface IWorkerService
  {
    IEnumerable<Worker> GetWorkers(bool trackChanges);
    Worker? GetWorker(int id, bool trackChanges);

    IEnumerable<Worker> GetWorkesrByProfessionId(int professionId, bool trackChanges);
    void AddWorker(Worker worker);
    void DeleteWorker(Worker worker);
    void UpdateWorker(Worker worker);

    IEnumerable<AvaliableTime> GetAvaliableTimesForWorker(int workerId, bool trackChanges);
  }
}