namespace PrintingHouse.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Core.Constants.ApplicationConstants;

    /// <summary>
    /// Admin area base controller
    /// </summary>
    [Area(nameof(AdminRoleName))]
    [Authorize(Roles = AdminRoleName)]
    [AutoValidateAntiforgeryToken]
    public class BaseController : Controller
    {
    }
}
