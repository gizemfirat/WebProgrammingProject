using Entities.Models;

namespace Services.Contracts
{
  public interface IProfessionService
  {
    IEnumerable<Profession> GetProfessions(bool trackChanges);
    Profession? GetProfession(int id, bool trackChanges);
    void AddProfession(Profession profession);
    void DeleteProfession(int professionId);
    void UpdateProfession(Profession profession);
    bool HasProcesses(int professionId);
    Task<String> DeleteProfessionAsync(int professionId);
  }
}