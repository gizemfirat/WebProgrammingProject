using Entities.Models;
using Entities.ViewModels;
using Microsoft.Identity.Client;

namespace Services.Contracts
{
  public interface IWorkerService
  {
    IEnumerable<Worker> GetWorkers(bool trackChanges);
    Worker? GetWorker(int id, bool trackChanges);
    void AddWorker(Worker worker);
    void DeleteWorker(Worker worker);
    void UpdateWorker(Worker worker);
    IEnumerable<Worker> GetWorkersByProcess(int processId);
    Task<IEnumerable<WorkerViewModel>> GetAllWorkersWithProcessesAsync();
    Task AddWorkerAsync(WorkerViewModel workerViewModel);
  }
}