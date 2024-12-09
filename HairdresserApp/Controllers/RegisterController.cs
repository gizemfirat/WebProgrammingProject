using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  public class RegisterController : Controller
  {

    private readonly IServiceManager _manager;

    public IActionResult Index() {
      return View();
    }
  }
}