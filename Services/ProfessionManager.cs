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
      if(profession is null) {
        throw new Exception("Profession not found");
      }

      return profession;
    }

     public void AddProfession(Profession profession)
        {
            _manager.Profession.AddProfession(profession);
        }

        public void DeleteProfession(Profession profession)
        {
            _manager.Profession.DeleteProfession(profession);
        }

        public void UpdateProfession(Profession profession)
        {
            _manager.Profession.UpdateProfession(profession);
        }
  }
}