namespace PrintingHouse.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return RedirectToActionPermanent("All", "Employee");
        }
    }
}
