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
      var avaliableTimes = _manager.WorkerService.GetAvaliableTimesForWorker(workerId, false);
      return View(avaliableTimes);
    }

    [HttpGet("MyAppointments")]
    public IActionResult MyAppointments() {
      if(string.IsNullOrEmpty(HttpContext.Session.GetString("customerId"))) {
        return RedirectToAction("Login", "Login");
      }

      int customerId = int.Parse(HttpContext.Session.GetString("customerId"));
      var appointments = _manager.AppointmentService.GetAppointmentsByCustomerId(customerId);

      return View(appointments);
    }

    [Route("Appointments/Delete")]
    [HttpPost]
    public IActionResult Delete(int id) {

      var appointment = _manager.AppointmentService.GetAppointment(id, false);
      if(appointment == null) {
        return NotFound();
      }

      _manager.AppointmentService.DeleteAppointment(id);
      return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(int id) {
      var result = await _manager.AppointmentService.DeleteAppointmentAysnc(id);
      if(!result) {
        return NotFound();
      }
      return NoContent();
    }

    [HttpGet("AvaliableTimes/{workerId}")]
    public IActionResult GetAvaliableTimesByWorker(int workerId) {
      var avaliableTimes = _manager.WorkerService.GetAvaliableTimesForWorker(workerId, false);
      return View(avaliableTimes);
    }

    [HttpPost("BookAppointment")]
    public IActionResult BookAppointment(int workerId, int dateId) {
      if(String.IsNullOrEmpty(HttpContext.Session.GetString("customerId"))) {
        return Unauthorized();
      }

      int customerId = int.Parse(HttpContext.Session.GetString("customerId"));

      var success = _manager.AppointmentService.BookAppointment(customerId, workerId, dateId);
      
      if(!success) {
        return BadRequest("Appointment could not be created.");
      }

      return Ok();
    }
  }
}