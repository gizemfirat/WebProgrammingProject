using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  public class WorkerController : Controller
  {
    private readonly IServiceManager _manager;

    public WorkerController(IServiceManager manager) {
      _manager = manager;
    }

    public IActionResult Index(int workerId) {
      var avaliableTimes = _manager.WorkerService.GetAvaliableTimesForWorker(workerId, false);

      return View(avaliableTimes);
    }
  }
}