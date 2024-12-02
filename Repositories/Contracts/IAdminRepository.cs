using System.Runtime.CompilerServices;
using Entities.Models;

namespace Repositories.Contracts
{
  public interface IAdminRepository : IRepositoryBase<Admin>
  {
    IQueryable<Admin> GetAllAdmins(bool trackChanges);
    Admin? GetAdmin(int id, bool trackChanges);
    void AddAdmin(Admin admin);
    void DeleteAdmin(Admin admin);
    void UpdateAdmin(Admin admin);
  }
}