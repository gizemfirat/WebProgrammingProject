using System.Runtime.CompilerServices;
using HairdresserApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  public class AccountController : Controller
  {
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IServiceManager _manager;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IServiceManager manager)
    {
      _signInManager = signInManager;
      _userManager = userManager;
      _manager = manager;
    }
    [HttpGet]
    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }

      var user = await _userManager.FindByEmailAsync(model.Email);
      if (user == null)
      {
        var customer = await _manager.CustomerService.GetCustomerByEmailAsync(model.Email);
        if (customer == null)
        {
          var worker = await _manager.WorkerService.GetWorkerByEmailAsync(model.Email);
          if (worker == null)
          {
            ModelState.AddModelError(string.Empty, "Unvalid Login Attempt.");
            return View(model);
          }
          else
          {

            if (worker.Email == model.Email && worker.Password == model.Password)
            {
              HttpContext.Session.SetInt32("workerId", worker.Id);
              return RedirectToAction("Index", "Home");
            }
            else
            {
              ModelState.AddModelError(string.Empty, "Unvalid Login Attempt.");
              return View(model);
            }
          }
        }
        else
        {

          if (customer.Email == model.Email && customer.Password == model.Password)
          {

            HttpContext.Session.SetInt32("customerId", customer.Id);
            return RedirectToAction("Index", "Home");
          }
          else
          {
            ModelState.AddModelError(string.Empty, "Unvalid Login Attempt.");
            return View(model);
          }
        }
      }

      var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

      if (result.Succeeded)
      {
        if (await _userManager.IsInRoleAsync(user, "Admin"))
        {
          return RedirectToAction("AdminPanel", "Home");
        }

        return RedirectToAction("Account", "Login");
      }

      if (result.IsLockedOut)
      {
        ModelState.AddModelError(string.Empty, "Your account has been locked. Please try again later.");
      }
      else
      {
        ModelState.AddModelError(string.Empty, "Unvalid Login Attempt.");
      }

      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
      HttpContext.Session.Clear();
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index", "Home");
    }
  }
}