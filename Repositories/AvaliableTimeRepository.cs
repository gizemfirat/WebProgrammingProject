using Entities.Models;
using Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
  public class AvaliableTimeRepository : RepositoryBase<AvaliableTime>, IAvaliableTimeRepository
  {
    public AvaliableTimeRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<AvaliableTime> GetAllAvaliableTimes(bool trackChanges) => GetAll(trackChanges);

    public AvaliableTime? GetAvaliableTime(int id, bool trackChanges)
    {
      return Get(a => a.Id.Equals(id), trackChanges);
    }

    public void AddAvaliableTime(AvaliableTime avaliableTime)
    {
      Add(avaliableTime);
      _context.SaveChanges();
    }

    public void DeleteAvaliableTime(AvaliableTime avaliableTime)
    {
      Delete(avaliableTime);
      _context.SaveChanges();
    }

    public void UpdateAvaliableTime(AvaliableTime avaliableTime)
    {
      Update(avaliableTime);
      _context.SaveChanges();
    }

    public async Task<AvaliableTime> GetByIdAsync(int id)
    {
      return await _context.Set<AvaliableTime>().FindAsync(id);
    }

    public async Task UpdateAsync(AvaliableTime avaliableTime)
    {
      _context.Set<AvaliableTime>().Update(avaliableTime);
      await _context.SaveChangesAsync();
    }

    public IEnumerable<AvaliableTimeDto> GetAvaliableTimesByWorkerId(int workerId)
    {
      return (from at in _context.AvaliableTimes
              join wp in _context.WorkerProcesses on at.WorkerProcessId equals wp.Id
              join p in _context.Processes on wp.ProcessId equals p.Id
              join w in _context.Workers on wp.WorkerId equals w.Id
              where wp.WorkerId == workerId && at.IsAvaliable == 1
              select new AvaliableTimeDto
              {
                AvaliableTimeId = at.Id,
                ProcessName = p.Name,
                WorkerName = w.Name,
                WorkerSurname = w.Surname,
                Price = p.Price,
                Time = at.Time
              }).ToList();
    }

    public List<ProcessDetailViewModel> GetAvaliableTimesForProcess(int processId)
    {
      var result = (from process in _context.Processes
                    join workerProcess in _context.WorkerProcesses on processId equals workerProcess.ProcessId
                    join worker in _context.Workers on workerProcess.WorkerId equals worker.Id
                    join avaliableTime in _context.AvaliableTimes on workerProcess.Id equals avaliableTime.WorkerProcessId
                    where process.Id == processId && avaliableTime.IsAvaliable == 1
                    select new ProcessDetailViewModel
                    {
                      AvaliableTimeId = avaliableTime.Id,
                      ProcessName = process.Name,
                      WorkerName = $"{worker.Name} {worker.Surname}",
                      AvaliableTime = avaliableTime.Time,
                      EndTime = avaliableTime.EndTime,
                      Price = process.Price
                    }).ToList();

      return result;
    }

    public List<AvaliableTimeViewModel> GetAvaliableTimeDetails()
    {
      var result = (from avaliableTime in _context.AvaliableTimes
                    join workerProcess in _context.WorkerProcesses on avaliableTime.WorkerProcessId equals workerProcess.Id
                    join process in _context.Processes on workerProcess.ProcessId equals process.Id
                    join worker in _context.Workers on workerProcess.WorkerId equals worker.Id
                    select new AvaliableTimeViewModel
                    {
                      AvaliableTimeId = avaliableTime.Id,
                      WorkerProcessId = workerProcess.Id,
                      WorkerName = worker.Name,
                      WorkerSurname = worker.Surname,
                      WorkerId = worker.Id,
                      ProcessId = process.Id,
                      ProcessName = process.Name,
                      Time = avaliableTime.Time,
                      EndTime = avaliableTime.EndTime,
                      IsAvaliable = avaliableTime.IsAvaliable
                    }).ToList();

      return result;
    }

    public bool HasTimeConflict(int workerProcessId, DateTime startTime, DateTime endTime, int? excludeId = null)
    {
      var workerId = _context.WorkerProcesses
      .Where(wp => wp.Id == workerProcessId)
      .Select(wp => wp.WorkerId)
      .FirstOrDefault();

      if (workerId == 0)
      {
        return false;
      }

      var conflictingTimes = (from at in _context.AvaliableTimes
                              join wp in _context.WorkerProcesses on at.WorkerProcessId equals wp.Id
                              where wp.WorkerId == workerId
                                    && at.IsAvaliable == 1
                                    && (at.EndTime > startTime && at.Time < endTime)
                                    && (excludeId == null || at.Id != excludeId)
                              select at).Any();

      return conflictingTimes;
    }
  }
}