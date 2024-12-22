using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  [Route("WorkerProcess")]
  public class WorkerProcessController : Controller
  {
    private readonly IServiceManager _manager;

    public WorkerProcessController(IServiceManager manager)
    {
      _manager = manager;
    }
    [HttpGet("GetWorkerProcesses")]
    public IActionResult GetWorkerProcesses()
    {
      var workerProcesses = _manager.WorkerProcessService.GetWorkerProcessesDetail()
                            .Select(wp => new
                            {
                              Id = wp.WorkerProcessId,
                              Name = wp.WorkerProcessFullName
                            }).ToList();

      return Json(workerProcesses);
    }


  }
}