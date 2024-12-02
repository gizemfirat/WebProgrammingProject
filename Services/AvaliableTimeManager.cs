using Services.Contracts;
using Entities.Models;
using Repositories.Contracts;

namespace Services
{
  public class AvaliableTimeManager : IAvaliableTimeService
  {
    private readonly IRepositoryManager _manager;

    public AvaliableTimeManager(IRepositoryManager manager)
    {
      _manager = manager;
    }
    public IEnumerable<AvaliableTime> GetAvaliableTimes(bool trackChanges)
    {
      return _manager.AvaliableTime.GetAllAvaliableTimes(trackChanges);
    }
    public AvaliableTime? GetAvaliableTime(int id, bool trackChanges)
    {
      var avaliableTime = _manager.AvaliableTime.GetAvaliableTime(id, trackChanges);
      if(avaliableTime is null) {
        throw new Exception("AvaliableTime not found");
      }

      return avaliableTime;
    }

     public void AddAvaliableTime(AvaliableTime avaliableTime)
        {
            _manager.AvaliableTime.AddAvaliableTime(avaliableTime);
        }

        public void DeleteAvaliableTime(AvaliableTime avaliableTime)
        {
            _manager.AvaliableTime.DeleteAvaliableTime(avaliableTime);
        }

        public void UpdateAvaliableTime(AvaliableTime avaliableTime)
        {
            _manager.AvaliableTime.UpdateAvaliableTime(avaliableTime);
        }
  }
}