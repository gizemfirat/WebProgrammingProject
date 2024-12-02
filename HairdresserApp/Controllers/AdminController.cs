using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Contracts;
using Entities.Models;
using Services.Contracts;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HairdresserApp.Controllers
{
  public class AdminController : Controller
  {
    private IServiceManager _manager;

    public AdminController(IServiceManager manager) {
      _manager = manager;
    }

    public IActionResult Index() {
      var model = _manager.AdminService.GetAdmins(false).ToList();
      return View(model);
    }

    [HttpGet]
    public IActionResult Create() {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Admin admin) {
      if(ModelState.IsValid) {
        _manager.AdminService.AddAdmin(admin);
        return RedirectToAction("Index");
      }

      return View(admin);      
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id) {
      var admin = _manager.AdminService.GetAdmin(id, true);
      if(admin == null) {
        return NotFound();
      }

      _manager.AdminService.DeleteAdmin(admin);
      return RedirectToAction("Index");
    }
  }
}