using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  public class ProfessionController : Controller
  {
    private readonly IServiceManager _manager;

    public ProfessionController(IServiceManager manager) {
      _manager = manager;
    }
    public IActionResult Index() {
      return View();
    }
  }
}