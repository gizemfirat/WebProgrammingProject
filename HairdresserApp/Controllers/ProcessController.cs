using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  public class ProcessController : Controller
  {

    private readonly IServiceManager _manager;

    public ProcessController(IServiceManager manager) {
      _manager = manager;
    }

    public IActionResult Index() {
      var processes = _manager.ProcessService.GetProcesses(false);
      return View(processes);
    }

    public IActionResult GetProcesses() {
      var processes = _manager.ProcessService.GetProcesses(false)
      .Select(p => new 
      {
        Id = p.Id,
        Name = p.Name
      }).ToList();

      return Json(processes);
    }

    public IActionResult GetProcessesByProfession(int professionId) {
      var processes = _manager.ProcessService.GetProcesses(false)
      .Where(p => p.ProfessionId == professionId)
      .ToList();

      return View(processes);
    }

    public IActionResult ListProfessions() {
      var professions = _manager.ProfessionService.GetProfessions(false);
      var processes = _manager.ProcessService.GetProcesses(false);

      var groupedData = professions
      .Select(prof => new ProfessionViewModel {
        ProfessionName = prof.Name,
        Processes = processes
        .Where(proc => proc.ProfessionId == prof.Id)
        .Select(proc => new ProcessViewModel {
          ProcessName = proc.Name,
          Time = proc.Time,
          Price = proc.Price
        })
        .ToList()
      })
      .ToList();

      return View(groupedData); 
    }

    public IActionResult ViewWorkers(int processId) {
      var workers = _manager.WorkerProcessService.GetWorkerProcesses(false)
      .Where(wp => wp.ProcessId == processId)
      .Join(
        _manager.WorkerService.GetWorkers(false),
        wp => wp.WorkerId,
        w => w.Id,
        (wp, w) => new Worker {
          Id = w.Id,
          Name = w.Name,
          Surname = w.Surname,
          Salary = w.Salary
        }
      ).ToList();

      return View(workers);
    }

    public IActionResult AvaliableTimes(int processId) {
      var avaliableTimes = _manager.AvaliableTimeService.GetAvaliableTimesByProcess(processId);
      return View(avaliableTimes);
    }

    public IActionResult ProcessList() {
      var processes = _manager.ProcessService.GetGroupedProcesses();
      return View(processes);
    }
  }
}