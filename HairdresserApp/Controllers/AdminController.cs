using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  [Authorize(Roles = "Admin")]
  public class AdminController : Controller
  {
    private readonly IServiceManager _manager;

    public AdminController(IServiceManager manager)
    {
      _manager = manager;
    }
    public IActionResult Index()
    {
      return View();
    }

    public async Task<IActionResult> GetWorkers()
    {
      var workers = await _manager.WorkerService.GetAllWorkersWithProcessesAsync();
      return View(workers);
    }

    public IActionResult GetProfessions()
    {
      var professions = _manager.ProfessionService.GetProfessions(false);
      return View(professions);
    }
    public IActionResult GetProcesses()
    {
      var processes = _manager.ProcessService.GetProcessesWithProfession();
      return View(processes);
    }

    public IActionResult GetAvaliableTimes()
    {
      return View();
    }

    public IActionResult GetAppointments()
    {
      var appointmentDetails = _manager.AppointmentService.GetAppointmentDetails();
      return View(appointmentDetails);
    }

    public IActionResult GetCustomers()
    {
      var customers = _manager.CustomerService.GetCustomers(false);
      return View(customers);
    }

  }
}