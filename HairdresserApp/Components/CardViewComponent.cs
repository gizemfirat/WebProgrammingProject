using HairdresserApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairdresserApp
{
  [ViewComponent(Name ="Card")]
  public class CardViewComponent : ViewComponent
  {
    public IViewComponentResult Invoke(CardModel model) {
      return View(model);
    }
  }
}