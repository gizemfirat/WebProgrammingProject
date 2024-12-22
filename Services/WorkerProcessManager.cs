using Services.Contracts;
using Entities.Models;
using Repositories.Contracts;
using Entities.ViewModels;

namespace Services
{
  public class WorkerProcessManager : IWorkerProcessService
  {
    private readonly IRepositoryManager _manager;

    public WorkerProcessManager(IRepositoryManager manager)
    {
      _manager = manager;
    }
    public IEnumerable<WorkerProcess> GetWorkerProcesses(bool trackChanges)
    {
      return _manager.WorkerProcess.GetAllWorkerProcesses(trackChanges);
    }
    public WorkerProcess? GetWorkerProcess(int id, bool trackChanges)
    {
      var WorkerProcess = _manager.WorkerProcess.GetWorkerProcess(id, trackChanges);
      if(WorkerProcess is null) {
        throw new Exception("WorkerProcess not found");
      }

      return WorkerProcess;
    }

     public void AddWorkerProcess(WorkerProcess workerProcess)
        {
            _manager.WorkerProcess.AddWorkerProcess(workerProcess);
        }

        public void DeleteWorkerProcess(WorkerProcess workerProcess)
        {
            _manager.WorkerProcess.DeleteWorkerProcess(workerProcess);
        }

        public void UpdateWorkerProcess(WorkerProcess workerProcess)
        {
            _manager.WorkerProcess.UpdateWorkerProcess(workerProcess);
        }

        public List<ProcessDto> GetProcessesByWorkerId(int workerId)
        {
          return _manager.WorkerProcess.GetProcessesByWorkerId(workerId);
        }

        public List<WorkerProcessViewModel> GetWorkerProcessesDetail()
        {
          return _manager.WorkerProcess.GetWorkerProcesses();
        }
    }
}