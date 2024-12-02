using Microsoft.AspNetCore.Mvc;

namespace HairdresserApp.Controllers
{
  public class LoginController : Controller
  {
    public IActionResult Index() {
      return View();
    }
  }
}