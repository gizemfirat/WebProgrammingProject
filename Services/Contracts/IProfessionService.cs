using Entities.Models;

namespace Services.Contracts
{
  public interface IProfessionService
  {
    IEnumerable<Profession> GetProfessions(bool trackChanges);
    Profession? GetProfession(int id, bool trackChanges);
    void AddProfession(Profession profession);
    void DeleteProfession(Profession profession);
    void UpdateProfession(Profession profession);
  }
}