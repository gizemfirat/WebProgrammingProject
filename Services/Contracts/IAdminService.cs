using Entities.Models;

namespace Services.Contracts
{
  public interface IAdminService
  {
    IEnumerable<Admin> GetAdmins(bool trackChanges);
    Admin? GetAdmin(int id, bool trackChanges);

    void AddAdmin(Admin admin);
    void DeleteAdmin(Admin admin);
    void UpdateAdmin(Admin admin);
  }
}