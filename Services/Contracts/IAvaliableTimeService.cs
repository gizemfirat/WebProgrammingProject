using Entities.Models;

namespace Services.Contracts
{
  public interface IAvaliableTimeService
  {
    IEnumerable<AvaliableTime> GetAvaliableTimes(bool trackChanges);
    AvaliableTime? GetAvaliableTime(int id, bool trackChanges);
    void AddAvaliableTime(AvaliableTime avaliableTime);
    void DeleteAvaliableTime(AvaliableTime avaliableTime);
    void UpdateAvaliableTime(AvaliableTime avaliableTime);
  }
}