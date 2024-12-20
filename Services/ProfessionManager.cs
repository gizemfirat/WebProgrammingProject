using Services.Contracts;
using Entities.Models;
using Repositories.Contracts;

namespace Services
{
  public class ProfessionManager : IProfessionService
  {
    private readonly IRepositoryManager _manager;

    public ProfessionManager(IRepositoryManager manager)
    {
      _manager = manager;
    }
    public IEnumerable<Profession> GetProfessions(bool trackChanges)
    {
      return _manager.Profession.GetAllProfessions(trackChanges);
    }
    public Profession? GetProfession(int id, bool trackChanges)
    {
      var profession = _manager.Profession.GetProfession(id, trackChanges);
      if (profession is null)
      {
        throw new Exception("Profession not found");
      }

      return profession;
    }

    public void AddProfession(Profession profession)
    {
      _manager.Profession.AddProfession(profession);
    }

    public void DeleteProfession(int professionId)
    {
      var profession = _manager.Profession.GetProfession(professionId, false);
      if (profession == null)
      {
        throw new Exception("Profession not found!");
      }

      _manager.Profession.DeleteProfession(profession);
    }

    public void UpdateProfession(Profession profession)
    {
      _manager.Profession.UpdateProfession(profession);
    }

    public bool HasProcesses(int professionId)
    {
      return _manager.Profession.HasProcesses(professionId);
    }

    public async Task<string> DeleteProfessionAsync(int professionId)
    {
      if(await _manager.Profession.HasProcessesAsync(professionId))
      {
        return "This profession cannot be deleted. First, delete the processes assosiated with this profession.";
      }

      await _manager.Profession.DeleteProfessionAsync(professionId);
      return "Profession deleted successfully!";
    }
  }
}