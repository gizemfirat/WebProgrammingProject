using Services.Contracts;
using Entities.Models;
using Repositories.Contracts;

namespace Services
{
  public class CustomerManager : ICustomerService
  {
    private readonly IRepositoryManager _manager;

    public CustomerManager(IRepositoryManager manager)
    {
      _manager = manager;
    }
    public IEnumerable<Customer> GetCustomers(bool trackChanges)
    {
      return _manager.Customer.GetAllCustomers(trackChanges);
    }
    public Customer? GetCustomer(int id, bool trackChanges)
    {
      var customer = _manager.Customer.GetCustomer(id, trackChanges);
      if(customer is null) {
        throw new Exception("Customer not found");
      }

      return customer;
    }

     public void AddCustomer(Customer customer)
        {
            _manager.Customer.AddCustomer(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            _manager.Customer.DeleteCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _manager.Customer.UpdateCustomer(customer);
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            return await _manager.Customer.GetCustomerByEmailAsync(email);
        }
    }
}