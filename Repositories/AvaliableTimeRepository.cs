using Entities.Models;
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
  }
}