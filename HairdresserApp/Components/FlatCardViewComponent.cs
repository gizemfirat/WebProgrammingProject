using HairdresserApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairdresserApp
{
  [ViewComponent(Name ="FlatCard")]
  public class FlatCardViewComponent : ViewComponent
  {
    public IViewComponentResult Invoke(FlatCardModel model) {
      return View(model);
    }
  }
}