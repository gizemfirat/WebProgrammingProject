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
      if(string.IsNullOrEmpty(HttpContext.Session.GetString("customerId"))) {
        return RedirectToAction("Login", "Login");
      }

      int customerId = int.Parse(HttpContext.Session.GetString("customerId"));
      var appointments = _manager.AppointmentService.GetAppointmentsByCustomer(customerId);

      return View(appointments);
    }

    [HttpPost("BookAppointment")]
    public IActionResult BookAppointment([FromBody] AppointmentRequest request) {
      var customerId = HttpContext.Session.GetString("customerId");

      if(customerId == null) {
        return Unauthorized();
      }

      var success = _manager.AppointmentService.SaveAppointment(request.AvaliableTimeId, int.Parse(customerId));

      if(!success) {
        return BadRequest("Appointment could not be created.");
      }

      return Ok("Appointment successfully created.");
    }

  }
}