namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
