using Entities.Models;

namespace Repositories.Contracts
{
  public interface ICustomerRepository : IRepositoryBase<Customer>
  {
    IQueryable<Customer> GetAllCustomers(bool trackChanges);
    Customer? GetCustomer(int id, bool trackChanges);
    void AddCustomer(Customer customer);
    void DeleteCustomer(Customer customer);
    void UpdateCustomer(Customer customer);
    Task<Customer> GetCustomerByEmailAsync(String email);
  }
}