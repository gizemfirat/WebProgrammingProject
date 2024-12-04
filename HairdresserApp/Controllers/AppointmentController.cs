using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  public class AppointmentController : Controller
  {
    private readonly IServiceManager _manager;

    public AppointmentController(IServiceManager manager)
    {
      _manager = manager;
    }

    public IActionResult Index(int workerId)
    {
      var avaliableTimes = _manager.WorkerService.GetAvaliableTimesForWorker(workerId, false);
      return View(avaliableTimes);
    }

    public IActionResult MyAppointments() {
      string sessionCustomerId = HttpContext.Session.GetString("customerId");

      if(sessionCustomerId is null) {
        return RedirectToAction("Index", "Home");
      }

      int customerId = Convert.ToInt32(sessionCustomerId);

      var appointments = _manager.AppointmentService.GetAppointmentsByCustomerId(customerId);

      return View(appointments);
    }
  }
}