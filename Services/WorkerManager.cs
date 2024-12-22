using Services.Contracts;
using Entities.Models;
using Repositories.Contracts;
using Entities.ViewModels;

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

    public void Delete(Worker worker)
    {
      _manager.Worker.DeleteWorker(worker);
    }

    public void UpdateWorker(WorkerViewModel workerViewModel)
    {
      var worker = _manager.Worker.GetWorker(workerViewModel.Id, false);

      if(worker == null) {
        throw new Exception("Worker not found!");
      }

      worker.Name = workerViewModel.Name;
      worker.Surname = workerViewModel.Surname;
      worker.Email = workerViewModel.Email;
      worker.Password = workerViewModel.Password;
      worker.Salary = workerViewModel.Salary;

      var processIds = workerViewModel.Processes.Select(p => p.Id).ToList();
      _manager.Worker.UpdateWorkerProcesses(workerViewModel.Id, processIds);

      _manager.Worker.UpdateWorker(worker);
    }

    public IEnumerable<Worker> GetWorkersByProcess(int processId)
    {
      return _manager.Worker.GetWorkersByProcessId(processId);
    }

        public async Task<IEnumerable<WorkerViewModel>> GetAllWorkersWithProcessesAsync()
        {
            return await _manager.Worker.GetWorkersWithProcessesAsync();
        }

        public async Task AddWorkerAsync(WorkerViewModel workerViewModel)
        {
          var worker = new Worker
          {
            Name = workerViewModel.Name,
            Surname = workerViewModel.Surname,
            Email = workerViewModel.Email,
            Password = workerViewModel.Password,
            Salary = workerViewModel.Salary
          };

          var processIds = workerViewModel.Processes.Select(p => p.Id).ToList();
          await _manager.Worker.AddWorkerAsync(worker, processIds);
        }

        public bool CanDeleteProcess(int workerId, int processId)
        {
            return !_manager.Worker.IsProcessInAppointment(workerId, processId);
        }

        public void DeleteWorker(int workerId)
        {
          if(_manager.Worker.HasAppointment(workerId))
          {
            throw new Exception("Cannot delete the worker because there are appointments associated with them. First, delete these appointments.");

          }

          _manager.Worker.DeleteWorker(workerId);
        }

        public async Task<Worker> GetWorkerByEmailAsync(string email)
        {
            return await _manager.Worker.GetWorkerByEmailAsync(email);
        }
    }
}