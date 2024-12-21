using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  public class WorkerController : Controller
  {
    private readonly IServiceManager _manager;

    public WorkerController(IServiceManager manager)
    {
      _manager = manager;
    }

    public IActionResult Index(int professionId)
    {

      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] WorkerViewModel workerViewModel)
    {
      Console.WriteLine("Received WorkerViewModel: ");
      Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(workerViewModel));
      if (workerViewModel == null || !ModelState.IsValid)
      {
        Console.WriteLine("Invalid Data");
        return BadRequest("Invalid data!");
      }

      await _manager.WorkerService.AddWorkerAsync(workerViewModel);
      return Ok(new { message = "Worker added successfully" });
    }

    [HttpPut("/Worker/Update")]
    public IActionResult Update([FromBody] WorkerViewModel workerViewModel)
    {
      var existingProcesses = _manager.WorkerProcessService.GetProcessesByWorkerId(workerViewModel.Id)
      .Select(wp => wp.Id).ToList();

      var processesToDelete = existingProcesses
      .Except(workerViewModel.Processes.Select(p => p.Id))
      .ToList();

      foreach (var processId in processesToDelete)
      {
        if (!_manager.WorkerService.CanDeleteProcess(workerViewModel.Id, processId))
        {
          return BadRequest("Process cannot be deleted because it has an associated appointment. First, delete this appointment");
        }
      }

      try
      {
        _manager.WorkerService.UpdateWorker(workerViewModel);
        return Ok("Worker updated successfully!");

      } catch(Exception ex) {
        return BadRequest($"Error occurred: {ex.Message}");
      }
    }

    [HttpDelete("/Worker/Delete/{id}")]
    public IActionResult Delete(int id)
    {
      try{
        _manager.WorkerService.DeleteWorker(id);
         return Ok(new { message = "Worker deleted successfully." });
      } catch(Exception ex) {
         return BadRequest(new { message = $"Error occurred: {ex.Message}" });
      }

    }
  }
}