using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  public class ServicesController : Controller
  {
    private readonly IServiceManager _manager;

    public ServicesController(IServiceManager manager) {
      _manager = manager;
    }
    
    [HttpGet]
    public IActionResult Index() {
      var professions = _manager.ProfessionService.GetProfessions(false);
      return View(professions);
    }
  }
}