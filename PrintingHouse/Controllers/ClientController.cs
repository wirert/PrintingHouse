namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Core.Constants.RoleNamesConstants;

    public class ClientController : BaseController
    {
        [HttpGet]
        [Authorize(Roles = Merchant)]
        public IActionResult Add()
        {
            return View();
        }
    }
}
