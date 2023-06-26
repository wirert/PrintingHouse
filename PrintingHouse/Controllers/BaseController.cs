using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrintingHouse.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}
