using HairdresserApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairdresserApp
{
  [ViewComponent(Name ="MenuCard")]
  public class MenuCardViewComponent : ViewComponent
  {
    public IViewComponentResult Invoke(MenuCardModel model) {
      return View(model);
    }
  }
}