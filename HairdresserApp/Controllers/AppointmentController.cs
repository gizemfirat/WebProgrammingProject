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
    public IActionResult MyAppointments()
    {
      if (HttpContext.Session.GetInt32("customerId") is null)
      {
        return RedirectToAction("Login", "Login");
      }

      int customerId = (int)HttpContext.Session.GetInt32("customerId");
      var appointments = _manager.AppointmentService.GetAppointmentsByCustomer(customerId);

      return View(appointments);
    }

    [HttpPost("BookAppointment")]
    public IActionResult BookAppointment([FromBody] AppointmentRequest request)
    {
      var customerId = HttpContext.Session.GetInt32("customerId");

      if (customerId == null)
      {
        return Unauthorized();
      }

      var success = _manager.AppointmentService.SaveAppointment(request.AvaliableTimeId, (int)customerId);

      if (!success)
      {
        return BadRequest("You cannot make an appointment because you have another appointment during this time interval.");
      }

      return Ok("Appointment successfully created.");
    }

    public IActionResult Deneme(int processId)
    {
      var avaliableTimes = _manager.AvaliableTimeService.GetAvaliableTimesByProcess(processId);
      return View(avaliableTimes);
    }

    [HttpDelete("/Appointment/DeleteAppointment/{appointmentId}")]
    public IActionResult DeleteAppointment(int appointmentId)
    {
      var result = _manager.AppointmentService.DeleteAppointment(appointmentId);
      if (!result)
      {
        return BadRequest(new { message = "Error!" });
      }

      return Ok(new { message = "Appointment deleted successfully!" });
    }

    [Route("AssignedAppointments")]
    public IActionResult AssignedAppointments()
    {

      if (HttpContext.Session.GetInt32("workerId") is null)
      {
        return RedirectToAction("Login", "Account");
      }
      int workerId = (int)HttpContext.Session.GetInt32("workerId");
      var appointments = _manager.AppointmentService.GetAppointmentForWorkers(workerId);

      return View(appointments);
    }

    [HttpPost("/Appointment/Approve/{selectedAppointmentId}")]
    public IActionResult Approve(int selectedAppointmentId) {
      _manager.AppointmentService.ApproveAppointment(selectedAppointmentId);
      return Ok();
    }

    [HttpPost("/Appointment/Reject/{selectedAppointmentId}")]
    public IActionResult Reject(int selectedAppointmentId) {
      _manager.AppointmentService.RejectAppointment(selectedAppointmentId);
      return Ok();
    }

  }
}