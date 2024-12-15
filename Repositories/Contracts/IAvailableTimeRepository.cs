using Entities.Models;
using Entities.ViewModels;

namespace Repositories.Contracts
{
  public interface IAvaliableTimeRepository : IRepositoryBase<AvaliableTime>
  {
    IQueryable<AvaliableTime> GetAllAvaliableTimes(bool trackChanges);
    AvaliableTime? GetAvaliableTime(int id, bool trackChanges);
    void AddAvaliableTime(AvaliableTime avaliableTime);
    void DeleteAvaliableTime(AvaliableTime avaliableTime);
    void UpdateAvaliableTime(AvaliableTime avaliableTime);
    Task<AvaliableTime> GetByIdAsync(int id);
    Task UpdateAsync(AvaliableTime avaliableTime);
     IEnumerable<AvaliableTimeDto> GetAvaliableTimesByWorkerId(int workerId);
    List<ProcessDetailViewModel> GetAvaliableTimesForProcess(int processId);
  }
}