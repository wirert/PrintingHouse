using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrintingHouse.Controllers
{
    /// <summary>
    /// Base controller
    /// </summary>
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class BaseController : Controller
    {
        
    }
}
