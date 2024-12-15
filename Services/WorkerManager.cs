using Services.Contracts;
using Entities.Models;
using Repositories.Contracts;

namespace Services
{
  public class WorkerManager : IWorkerService
  {
    private readonly IRepositoryManager _manager;

    public WorkerManager(IRepositoryManager manager)
    {
      _manager = manager;
    }
    public IEnumerable<Worker> GetWorkers(bool trackChanges)
    {
      return _manager.Worker.GetAllWorkers(trackChanges);
    }
    public Worker? GetWorker(int id, bool trackChanges)
    {
      var worker = _manager.Worker.GetWorker(id, trackChanges);
      if (worker is null)
      {
        throw new Exception("Worker not found");
      }

      return worker;
    }

    public void AddWorker(Worker worker)
    {
      _manager.Worker.AddWorker(worker);
    }

    public void DeleteWorker(Worker worker)
    {
      _manager.Worker.DeleteWorker(worker);
    }

    public void UpdateWorker(Worker worker)
    {
      _manager.Worker.UpdateWorker(worker);
    }

    public IEnumerable<Worker> GetWorkersByProcess(int processId)
    {
      return _manager.Worker.GetWorkersByProcessId(processId);
    }
  }
}