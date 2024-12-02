using Services.Contracts;
using Entities.Models;
using Repositories.Contracts;

namespace Services
{
  public class AdminManager : IAdminService
  {
    private readonly IRepositoryManager _manager;

    public AdminManager(IRepositoryManager manager)
    {
      _manager = manager;
    }
    public IEnumerable<Admin> GetAdmins(bool trackChanges)
    {
      return _manager.Admin.GetAllAdmins(trackChanges);
    }
    public Admin? GetAdmin(int id, bool trackChanges)
    {
      var admin = _manager.Admin.GetAdmin(id, trackChanges);
      if(admin is null) {
        throw new Exception("Admin not found");
      }

      return admin;
    }

        public void AddAdmin(Admin admin)
        {
            _manager.Admin.AddAdmin(admin);
        }

        public void DeleteAdmin(Admin admin)
        {
            _manager.Admin.DeleteAdmin(admin);
        }

        public void UpdateAdmin(Admin admin)
        {
            _manager.Admin.UpdateAdmin(admin);
        }
    }
}