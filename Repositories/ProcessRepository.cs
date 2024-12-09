using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
  public class ProcessRepository : RepositoryBase<Process>, IProcessRepository
  {
    public ProcessRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<Process> GetAllProcesses(bool trackChanges) => GetAll(trackChanges);

    public Process? GetProcess(int id, bool trackChanges)
    {
      return Get(a => a.Id.Equals(id), trackChanges);
    }

    public void AddProcess(Process process)
    {
      Add(process);
      _context.SaveChanges();
    }

    public void DeleteProcess(Process process)
    {
      Delete(process);
      _context.SaveChanges();
    }

    public void UpdateProcess(Process process)
    {
      Update(process);
      _context.SaveChanges();
    }
  }
}