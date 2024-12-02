using Entities.Models;

namespace Repositories.Contracts
{
  public interface IAvaliableTimeRepository : IRepositoryBase<AvaliableTime>
  {
    IQueryable<AvaliableTime> GetAllAvaliableTimes(bool trackChanges);
    AvaliableTime? GetAvaliableTime(int id, bool trackChanges);
    void AddAvaliableTime(AvaliableTime avaliableTime);
    void DeleteAvaliableTime(AvaliableTime avaliableTime);
    void UpdateAvaliableTime(AvaliableTime avaliableTime);
  }
}