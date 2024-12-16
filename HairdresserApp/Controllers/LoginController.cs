using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  public class LoginController : Controller
  {

    private readonly IServiceManager _manager;

    public LoginController(IServiceManager manager) {
      _manager = manager;
    }
    public IActionResult Index() {
      if(HttpContext.Session.GetString("customerId") is not null) {
        return RedirectToAction("Index", "Home");
      }
      return View();
    }

    public IActionResult Login(string email, string password) {
      var customer = _manager.CustomerService.GetCustomers(false)
      .FirstOrDefault(c => c.Email == email && c.Password == password);

      if(customer != null) {
        HttpContext.Session.SetString("customerId", customer.Id.ToString());

        TempData["msj"] = @customer.Name + " " + @customer.Surname + " " + ", Welcome!";
        return RedirectToAction("Index", "Home");
      }

      TempData["msj"] = "Incorrect username or password. Please try again.";
      return RedirectToAction("Index");
    }

    public IActionResult Logout() {
      if(HttpContext.Session.GetString("customerId") is not null) {
        HttpContext.Session.Clear();
        TempData["msj"] = "You logged out successfully!";
      }

      return RedirectToAction("Index", "Home");
    }
  }
}