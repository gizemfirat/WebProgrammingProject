using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
    public class AdminRepository : RepositoryBase<Admin>, IAdminRepository
    {
        public AdminRepository(RepositoryContext context) : base(context)
        {
        }

        public IQueryable<Admin> GetAllAdmins(bool trackChanges) => GetAll(trackChanges);

        public Admin? GetAdmin(int id, bool trackChanges) {
          return Get(a => a.Id.Equals(id), trackChanges);
        }

        public void AddAdmin(Admin admin)
        {
          Add(admin);
          _context.SaveChanges();
        }

        public void DeleteAdmin(Admin admin)
        {
            Delete(admin);
            _context.SaveChanges();
        }

        public void UpdateAdmin(Admin admin)
        {
            Update(admin);
            _context.SaveChanges();
        }
    }
}