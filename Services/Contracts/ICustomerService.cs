using Entities.Models;

namespace Services.Contracts
{
  public interface ICustomerService
  {
    IEnumerable<Customer> GetCustomers(bool trackChanges);
    Customer? GetCustomer(int id, bool trackChanges);
    void AddCustomer(Customer customer);
    void DeleteCustomer(Customer customer);
    void UpdateCustomer(Customer customer);
    Task<Customer> GetCustomerByEmailAsync(String email);
    bool DeleteCustomerWithDependenciesAsync(int customerId); 
  }
}