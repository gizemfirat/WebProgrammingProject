using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  
    public class ProcessController : Controller
  {

    private readonly IServiceManager _manager;

    public ProcessController(IServiceManager manager)
    {
      _manager = manager;
    }

    public IActionResult Index()
    {
      var processes = _manager.ProcessService.GetProcesses(false);
      return View(processes);
    }

    public IActionResult GetProcesses()
    {
      var processes = _manager.ProcessService.GetProcesses(false)
      .Select(p => new
      {
        Id = p.Id,
        Name = p.Name
      }).ToList();

      return Json(processes);
    }

    public IActionResult GetProcessesByProfession(int professionId)
    {
      var processes = _manager.ProcessService.GetProcesses(false)
      .Where(p => p.ProfessionId == professionId)
      .ToList();

      return View(processes);
    }

    public IActionResult ListProfessions()
    {
      var professions = _manager.ProfessionService.GetProfessions(false);
      var processes = _manager.ProcessService.GetProcesses(false);

      var groupedData = professions
      .Select(prof => new ProfessionViewModel
      {
        ProfessionName = prof.Name,
        Processes = processes
        .Where(proc => proc.ProfessionId == prof.Id)
        .Select(proc => new ProcessViewModel
        {
          ProcessName = proc.Name,
          Time = proc.Time,
          Price = proc.Price
        })
        .ToList()
      })
      .ToList();

      return View(groupedData);
    }

    public IActionResult ViewWorkers(int processId)
    {
      var workers = _manager.WorkerProcessService.GetWorkerProcesses(false)
      .Where(wp => wp.ProcessId == processId)
      .Join(
        _manager.WorkerService.GetWorkers(false),
        wp => wp.WorkerId,
        w => w.Id,
        (wp, w) => new Worker
        {
          Id = w.Id,
          Name = w.Name,
          Surname = w.Surname,
          Salary = w.Salary
        }
      ).ToList();

      return View(workers);
    }

    public IActionResult AvaliableTimes(int processId)
    {
      var avaliableTimes = _manager.AvaliableTimeService.GetAvaliableTimesByProcess(processId);
      return View(avaliableTimes);
    }

    public IActionResult ProcessList()
    {
      var processes = _manager.ProcessService.GetGroupedProcesses();
      return View(processes);
    }

    [HttpPost("Process/Create")]
    public IActionResult Create([FromBody] Process request)
    {
      if (request == null || !ModelState.IsValid)
      {
        return BadRequest("Invalid Data");
      }

      _manager.ProcessService.AddProcess(request);
      return Ok(new { message = "Process Added successfully!" });
    }

    [HttpPut("/Process/Update")]
    public IActionResult Update([FromBody] Process request)
    {
      if (request == null || request.Id == null || request.Id <= 0)
      {
        return BadRequest("Invalid Data");
      }

      var existingProcess = _manager.ProcessService.GetProcess(request.Id, false);
      if(existingProcess == null) {
        return NotFound("Process not found!");
      }

      existingProcess.Name = request.Name;
      existingProcess.Price = request.Price;
      existingProcess.Time = request.Time;
      existingProcess.ProfessionId = request.ProfessionId;

      _manager.ProcessService.UpdateProcess(existingProcess);
      return Ok(new {message = "Process updated succesfully"});
    }

    [HttpDelete("/Process/Delete/{processId}")]
    public IActionResult Delete(int processId) {
      if(_manager.ProcessService.CheckIfProcessHasAppointments(processId)) {
        return BadRequest(new
        {
          message = "There's an appointment with this process. Please delete this appointment first."
        });
      }

      _manager.ProcessService.RemoveProcess(processId);
      return Ok(new {message= "Process deleted successfully"});
    }
  }
}