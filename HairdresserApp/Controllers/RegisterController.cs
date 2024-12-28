using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  public class RegisterController : Controller
  {

    private readonly IServiceManager _manager;

    public RegisterController(IServiceManager manager) {
      _manager = manager;
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
      if (ModelState.IsValid)
      {
        var customer = new Customer
        {
          Name = model.Name,
          Surname = model.Surname,
          Email = model.Email,
          Password = model.Password
        };

        _manager.CustomerService.AddCustomer(customer);

        TempData["errormsj"] = "Signed up successfully, please log in.";
        return RedirectToAction("Login", "Account");
      }

      return View("Index", model);

    }
  }
}