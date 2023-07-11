using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrintingHouse.Controllers
{
    [Authorize]
    //[AutoValidateAntiforgeryToken]
    public class BaseController : Controller
    {
        
    }
}
