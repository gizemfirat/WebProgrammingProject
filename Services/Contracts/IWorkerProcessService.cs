using Entities.Models;
using Entities.ViewModels;

namespace Services.Contracts
{
  public interface IWorkerProcessService
  {
    IEnumerable<WorkerProcess> GetWorkerProcesses(bool trackChanges);
    WorkerProcess? GetWorkerProcess(int id, bool trackChanges);
    void AddWorkerProcess(WorkerProcess workerProcess);
    void DeleteWorkerProcess(WorkerProcess workerProcess);
    void UpdateWorkerProcess(WorkerProcess workerProcess);
    List<ProcessDto> GetProcessesByWorkerId(int workerId);
  }
}