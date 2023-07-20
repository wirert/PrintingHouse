namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PrintingHouse.Core.Models.Order;
    using PrintingHouse.Core.Services.Contracts;
    using static Core.Constants.MessageConstants;

    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(
            IOrderService _orderService)
        {
            orderService = _orderService;            
        }

        public IActionResult Index()
        {
            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Create(Guid articleId)
        {
            try
            {
                var model = await orderService.CreateAddModelByArticleIdAsync(articleId);

                return View(model);
            }
            catch (Exception)
            {
                TempData[WarningMessage] = "Invalid article. Try again!";
                return RedirectToAction("All", "Article");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddOrderViewModel model)
        {
            if (model.EndDate < DateTime.Now.Date) 
            {
                ModelState.AddModelError(nameof(model.EndDate), "The deadline can't be before today!");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {

            }
            catch (Exception)
            {

                throw;
            }

            return View(model);
        }
    }
}
