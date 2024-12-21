using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace HairdresserApp.Controllers
{
  [Route("Profession")]
  public class ProfessionController : Controller
  {
    private readonly IServiceManager _manager;

    public ProfessionController(IServiceManager manager) {
      _manager = manager;
    }

    [HttpGet("GetProfessions")]
    public IActionResult GetProfessions() {
      var professions = _manager.ProfessionService.GetProfessions(false)
      .Select(p => new 
      {
        Id = p.Id,
        Name = p.Name
      }).ToList();

      return Json(professions);
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

    [HttpDelete("DeleteProfession/{professionId}")]
    public async Task<IActionResult> DeleteProfession(int professionId)
    {
      var resultMessage = await _manager.ProfessionService.DeleteProfessionAsync(professionId);

      if(resultMessage == "Profession deleted successfully!") {
        return Json(new {succes = true, message = resultMessage});
      }

      return Json(new {success = false, message = resultMessage});
    }

  }
}