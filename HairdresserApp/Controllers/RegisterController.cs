using Microsoft.AspNetCore.Mvc;

namespace HairdresserApp.Controllers
{
  public class RegisterController : Controller
  {
    public IActionResult Index() {
      return View();
    }
  }
}