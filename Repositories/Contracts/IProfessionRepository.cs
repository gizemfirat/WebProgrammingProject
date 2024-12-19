using Entities.Models;

namespace Repositories.Contracts
{
  public interface IProfessionRepository : IRepositoryBase<Profession>
  {
    IQueryable<Profession> GetAllProfessions(bool trackChanges);
    Profession? GetProfession(int id, bool trackChanges);
    void AddProfession(Profession profession);
    void DeleteProfession(Profession profession);
    void UpdateProfession(Profession profession);
    bool HasProcesses(int professionId);
  }
}