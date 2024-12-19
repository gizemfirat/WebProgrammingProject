using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  public class ProfessionController : Controller
  {
    private readonly IServiceManager _manager;

    public ProfessionController(IServiceManager manager) {
      _manager = manager;
    }
    public IActionResult Index() {
      return View();
    }

    [HttpPost("/Profession/Create")]
    public IActionResult Create([FromBody] Profession request) {

      ModelState.Remove("Id");

      if(request == null || !ModelState.IsValid) {
        return BadRequest("Invalid Data!");
      }

      _manager.ProfessionService.AddProfession(request);
      return Ok(new {message = "Profession added successfully"});
    }

    [HttpPut("/Profession/Update")]
    public IActionResult Update([FromBody] Profession request) {
      if(request == null || request.Id == null || request.Id <= 0) {
        return BadRequest("Invalid Data");
      }

      var existProfession = _manager.ProfessionService.GetProfession(request.Id, false);
      if(existProfession == null) {
        return NotFound("Profession not found");
      }

      existProfession.Name = request.Name;
      existProfession.Description = request.Description;

      _manager.ProfessionService.UpdateProfession(existProfession);
      return Ok("Profession updated successfully");
    }

    [HttpGet("/api/Profession/HasProcesses")]
    public IActionResult HasProcesses(int professionId) {

      bool hasProcesses = _manager.ProfessionService.HasProcesses(professionId);
      return Ok(new { hasProcesses });
    }

    [HttpDelete("/Profession/Delete")]
    public IActionResult Delete(int professionId) {
      try
      {
        _manager.ProfessionService.DeleteProfession(professionId);
        return Ok();
      }
      catch(Exception ex) {
        return BadRequest(ex.Message);
      }
    }
  }
}