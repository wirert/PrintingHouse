namespace PrintingHouse.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using static Core.Constants.RoleNamesConstants;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return RedirectToAction("All", "Employee");
        }
    }
}
