using HairdresserApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairdresserApp
{
  [ViewComponent(Name ="EmployeeCard")]
  public class EmployeeCardViewComponent : ViewComponent
  {
    public IViewComponentResult Invoke(CardModel model) {
      return View(model);
    }
  }
}