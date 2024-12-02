using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  public class AppointmentController : Controller
  {
    private readonly IServiceManager _manager;

    public AppointmentController(IServiceManager manager) {
      _manager = manager;
    }

    public IActionResult Index(int professionId) {
      var workers = _manager.WorkerService.GetWorkesrByProfessionId(professionId, false);
      return View(workers);
    }


  }
}