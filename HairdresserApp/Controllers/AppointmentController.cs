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
      if(string.IsNullOrEmpty(HttpContext.Session.GetString("customerId"))) {
        return RedirectToAction("Login", "Login");
      }

      int customerId = int.Parse(HttpContext.Session.GetString("customerId"));
      var appointments = _manager.AppointmentService.GetAppointmentsByCustomerId(customerId);

      return View(appointments);
    }
    [HttpPost]
    public IActionResult Delete(int id) {
      _manager.AppointmentService.DeleteAppointment(id);
      return Ok();
    }
  }
}