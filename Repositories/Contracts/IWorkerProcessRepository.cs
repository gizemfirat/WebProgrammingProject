using System.ComponentModel;
using Entities.Models;
using Entities.ViewModels;

namespace Repositories.Contracts
{
  public interface IWorkerProcessRepository : IRepositoryBase<WorkerProcess>
  {
    IQueryable<WorkerProcess> GetAllWorkerProcesses(bool trackChanges);
    WorkerProcess? GetWorkerProcess(int id, bool trackChanges);
    void AddWorkerProcess(WorkerProcess workerProcess);
    void DeleteWorkerProcess(WorkerProcess workerProcess);
    void UpdateWorkerProcess(WorkerProcess workerProcess);
    List<ProcessDto> GetProcessesByWorkerId(int workerId);
    List<WorkerProcessViewModel> GetWorkerProcesses();
  }
}