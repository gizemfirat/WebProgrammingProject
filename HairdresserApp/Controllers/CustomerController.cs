using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  public class CustomerController : Controller
  {
    private readonly IServiceManager _manager;

    public CustomerController(IServiceManager manager) {
      _manager = manager;
    }

    [HttpPost("/Customer/Create")]
    public IActionResult Create([FromBody] Customer request) {
      ModelState.Remove("Role");

      if(request == null | !ModelState.IsValid) {
        return BadRequest("Invalid Data!");
      }

      _manager.CustomerService.AddCustomer(request);
      return Ok(new {message = "Customer added successfully"});
    }

    [HttpPut("/Customer/Update")]
    public IActionResult Update([FromBody] Customer request)
    {
      if(request == null || request.Id == null || request.Id <= 0)
      {
        return BadRequest("Invalid Data");
      }

      var existingCustomer = _manager.CustomerService.GetCustomer(request.Id, false);
      if(existingCustomer == null) {
        return NotFound("Customer not found.");
      }

      existingCustomer.Name = request.Name;
      existingCustomer.Surname = request.Surname;
      existingCustomer.Email = request.Email;
      existingCustomer.Password = request.Password;
      existingCustomer.Role = request.Role;

      _manager.CustomerService.UpdateCustomer(existingCustomer);
      return Ok("Customer updated successfully!");
    }

    [HttpDelete("/Customer/DeleteCustomer/{customerId}")]
    public IActionResult DeleteCustomer(int customerId) {
      try
      {
        var result = _manager.CustomerService.DeleteCustomerWithDependenciesAsync(customerId);
        if(!result) {
          return BadRequest("Invalid Data or another error!");
        }

        return RedirectToAction("GetCustomers", "Admin");

      } catch(Exception ex) {
        return BadRequest($"An error occured: {ex.Message}");
      }
    }
  }
}