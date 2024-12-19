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

    public IActionResult Create([FromBody] Profession request) {
      if(request == null || !ModelState.IsValid) {
        return BadRequest("Invalid Data!");
      }

      _manager.ProfessionService.AddProfession(request);
      return Ok(new {message = "Profession added successfully"});
    }
  }
}