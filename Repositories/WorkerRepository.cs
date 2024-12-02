using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
  public class WorkerRepository : RepositoryBase<Worker>, IWorkerRepository
  {
    public WorkerRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<Worker> GetAllWorkers(bool trackChanges) => GetAll(trackChanges);

    public Worker? GetWorker(int id, bool trackChanges)
    {
      return Get(a => a.Id.Equals(id), trackChanges);
    }

    public void AddWorker(Worker worker)
    {
      Add(worker);
      _context.SaveChanges();
    }

    public void DeleteWorker(Worker worker)
    {
      Delete(worker);
      _context.SaveChanges();
    }

    public void UpdateWorker(Worker worker)
    {
      Update(worker);
      _context.SaveChanges();
    }

        public IEnumerable<Worker> GetWorkersByProfessionId(int professionId, bool trackChanges)
        {
            return trackChanges
            ? _context.Set<Worker>().Where(w => w.Profession == professionId).ToList()
            : _context.Set<Worker>().Where(w => w.Profession == professionId).AsNoTracking().ToList();
        }

        public IEnumerable<AvaliableTime> GetAvaliableTimesForWorker(int workerId, bool trackChanges)
        {
          return trackChanges
          ? _context.AvaliableTimes.Where(at => at.Worker == workerId).ToList()
          : _context.AvaliableTimes.Where(at => at.Worker == workerId).AsNoTracking().ToList();
        }
    }
}