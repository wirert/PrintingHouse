namespace PrintingHouse.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Core.Constants.RoleNamesConstants;

    /// <summary>
    /// Admin area base controller
    /// </summary>
    [Area(nameof(Admin))]
    [Authorize(Roles = Admin)]
    [AutoValidateAntiforgeryToken]
    public class BaseController : Controller
    {
    }
}
