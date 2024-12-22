using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  public class AvaliableTimeController : Controller
  {
    private readonly IServiceManager _manager;

    public AvaliableTimeController(IServiceManager manager) {
      _manager = manager;
    }

    [HttpPost("AvaliableTime/Create")]
    public IActionResult Create([FromBody] AvaliableTime request)
    {
      if (request == null || !ModelState.IsValid)
      {
        return BadRequest("Invalid Data");
      }

      _manager.AvaliableTimeService.AddAvaliableTime(request);
      return Ok(new { message = "AvaliableTime Added successfully!" });
    }

    [HttpPut("/AvaliableTime/Update")]
    public IActionResult Update([FromBody] AvaliableTime request)
    {
      if (request == null || request.Id == null || request.Id <= 0)
      {
        return BadRequest("Invalid Data");
      }

      var existingAvaliableTime = _manager.AvaliableTimeService.GetAvaliableTime(request.Id, false);
      if(existingAvaliableTime == null) {
        return NotFound("AvaliableTime not found!");
      }

      if(existingAvaliableTime.IsAvaliable == 0) {
        return BadRequest("You can't update this time because there's an appointment with this time.");
      }

      existingAvaliableTime.Time = request.Time;
      existingAvaliableTime.EndTime = request.EndTime;
      existingAvaliableTime.WorkerProcessId = request.WorkerProcessId;

      _manager.AvaliableTimeService.UpdateAvaliableTime(existingAvaliableTime);
      return Ok(new {message = "Avaliable Time updated succesfully"});
    }

    [HttpDelete("/AvaliableTime/Delete/{avaliableTimeId}")]
    public IActionResult Delete(int avaliableTimeId) {

      var avaliableTime = _manager.AvaliableTimeService.GetAvaliableTime(avaliableTimeId, false);

      if(avaliableTime.IsAvaliable == 0) {
        return BadRequest(new
        {
          message = "There's an appointment with this time. Please delete this appointment first."
        });
      }

      _manager.AvaliableTimeService.DeleteAvaliableTime(avaliableTime);
      return Ok(new {message= "Avaliable Time deleted successfully"});
    }
  }
}