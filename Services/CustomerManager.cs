using Services.Contracts;
using Entities.Models;
using Repositories.Contracts;
using System.Reflection.Metadata;

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

        public bool DeleteCustomerWithDependenciesAsync(int customerId)
        {
          var customer = _manager.Customer.GetCustomer(customerId, false);
          if(customer == null) {
            return false;
          }

          var appointments = _manager.Appointment.GetAppointmentsByCustomerId(customerId);
          foreach(var appointment in appointments) {
            var avaliableTime = _manager.AvaliableTime.GetAvaliableTime(appointment.AvaliableTimeId, false);
            if(avaliableTime != null) {
              avaliableTime.IsAvaliable = 1;
              _manager.AvaliableTime.UpdateAvaliableTime(avaliableTime);
            }

            _manager.Appointment.DeleteAppointment(appointment);
          }

          _manager.Customer.DeleteCustomer(customer);
          return true;
        }
    }
}