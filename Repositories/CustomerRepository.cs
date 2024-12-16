using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
  public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
  {
    public CustomerRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<Customer> GetAllCustomers(bool trackChanges) => GetAll(trackChanges);

    public Customer? GetCustomer(int id, bool trackChanges)
    {
      return Get(a => a.Id.Equals(id), trackChanges);
    }

    public void AddCustomer(Customer customer)
    {
      Add(customer);
      _context.SaveChanges();
    }

    public void DeleteCustomer(Customer customer)
    {
      Delete(customer);
      _context.SaveChanges();
    }

    public void UpdateCustomer(Customer customer)
    {
      Update(customer);
      _context.SaveChanges();
    }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            return await _context.Customers
            .Where(c => c.Email == email)
            .FirstOrDefaultAsync();
        }
    }
}