using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AppointmentController : Controller
  {
    private readonly IServiceManager _manager;

    public AppointmentController(IServiceManager manager)
    {
      _manager = manager;
    }

    [HttpGet("Index")]
    public IActionResult Index(int workerId)
    {
      var avaliableTimes = _manager.WorkerService.GetWorker(workerId, false);
      return View(avaliableTimes);
    }

    [HttpGet("MyAppointments")]
    public IActionResult MyAppointments() {
      if(HttpContext.Session.GetInt32("customerId") is null) {
        return RedirectToAction("Login", "Login");
      }

      int customerId = (int)HttpContext.Session.GetInt32("customerId");
      var appointments = _manager.AppointmentService.GetAppointmentsByCustomer(customerId);

      return View(appointments);
    }

    [HttpPost("BookAppointment")]
    public IActionResult BookAppointment([FromBody] AppointmentRequest request) {
      var customerId = HttpContext.Session.GetInt32("customerId");

      if(customerId == null) {
        return Unauthorized();
      }

      var success = _manager.AppointmentService.SaveAppointment(request.AvaliableTimeId, (int)customerId);

      if(!success) {
        return BadRequest("Appointment could not be created.");
      }

      return Ok("Appointment successfully created.");
    }

      public IActionResult Deneme(int processId) {
      var avaliableTimes = _manager.AvaliableTimeService.GetAvaliableTimesByProcess(processId);
      return View(avaliableTimes);
    }

  }
}