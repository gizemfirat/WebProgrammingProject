using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  public class WorkerController : Controller
  {
    private readonly IServiceManager _manager;

    public WorkerController(IServiceManager manager) {
      _manager = manager;
    }

    public IActionResult Index(int professionId) {

      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] WorkerViewModel workerViewModel) {
      Console.WriteLine("Received WorkerViewModel: ");
      Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(workerViewModel));
      if(workerViewModel == null || !ModelState.IsValid)
      {
        Console.WriteLine("Invalid Data");
        return BadRequest("Invalid data!");
      }

      await _manager.WorkerService.AddWorkerAsync(workerViewModel);
      return Ok(new {message = "Worker added successfully"});
    }
  }
}