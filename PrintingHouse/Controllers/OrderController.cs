namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PrintingHouse.Core.Models.Order;
    using PrintingHouse.Core.Services.Contracts;
    using PrintingHouse.Infrastructure.Data.Entities.Enums;
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
        public async Task<IActionResult> All()
        {
            try
            {
                var models = await orderService.GetAllOrdersAsync();

                return View(models);
            }
            catch (Exception)
            {
                TempData[WarningMessage] = "Unable to load orders! Try again";

                return RedirectToAction("Index", "Home");
            }
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
                await orderService.CreateOrder(model);

                TempData[SuccessMessage] = "Order created successfully.";

                return RedirectToAction("All");
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = "Something went wrong trying to create an order! Try again.";

                return RedirectToAction("All", "Article");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, OrderStatus status)
        {
            try
            {
                await orderService.ChangeStatusAsync(id, status);

                TempData[SuccessMessage] = $"Status changed to {status}";
            }
            catch (ArgumentException ae)
            {
                TempData[WarningMessage] = ae.Message;
            }
            catch (Exception)
            {
                TempData[WarningMessage] = "Problem occurred! Try again.";
            }

            return RedirectToAction("All");
        }
    }
}
