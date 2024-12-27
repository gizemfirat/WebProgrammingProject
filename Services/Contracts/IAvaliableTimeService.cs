using Entities.Models;
using Entities.ViewModels;

namespace Services.Contracts
{
  public interface IAvaliableTimeService
  {
    IEnumerable<AvaliableTime> GetAvaliableTimes(bool trackChanges);
    AvaliableTime? GetAvaliableTime(int id, bool trackChanges);
    void AddAvaliableTime(AvaliableTime avaliableTime);
    void DeleteAvaliableTime(AvaliableTime avaliableTime);
    void UpdateAvaliableTime(AvaliableTime avaliableTime);
    IEnumerable<AvaliableTimeDto> GetAvaliableiTimesByWorker(int workerId);
    List<ProcessDetailViewModel> GetAvaliableTimesByProcess(int processId);
    List<AvaliableTimeViewModel> GetAvaliableTimeDetails();
    bool HasTimeConflict(int workerProcessId, DateTime startTime, DateTime endTime, int? excludeId = null);
  }
}