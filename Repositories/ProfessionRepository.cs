using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
  public class ProfessionRepository : RepositoryBase<Profession>, IProfessionRepository
  {
    public ProfessionRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<Profession> GetAllProfessions(bool trackChanges) => GetAll(trackChanges);

    public Profession? GetProfession(int id, bool trackChanges)
    {
      return Get(a => a.Id.Equals(id), trackChanges);
    }

    public void AddProfession(Profession profession)
    {
      Add(profession);
      _context.SaveChanges();
    }

    public void DeleteProfession(Profession profession)
    {
      Delete(profession);
      _context.SaveChanges();
    }

    public void UpdateProfession(Profession profession)
    {
      Update(profession);
      _context.SaveChanges();
    }
  }
}