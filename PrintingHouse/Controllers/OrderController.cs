namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Core.Exceptions;
    using Core.Models.Order;
    using Core.Services.Contracts;
    using static Core.Constants.MessageConstants;
    using static Core.Constants.ApplicationConstants;
    using Infrastructure.Data.Entities.Enums;

    /// <summary>
    /// Order controller
    /// </summary>
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly ILogger logger;

        public OrderController(
            IOrderService _orderService,
            ILogger<OrderController> _logger)
        {
            orderService = _orderService;
            logger = _logger;
        }

        /// <summary>
        /// Redirects to 'All' action
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return RedirectPermanent("All");
        }

        /// <summary>
        /// Takes all orders and pass it to the view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var models = await orderService.GetAllOrdersAsync();

                return View(models);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                TempData[WarningMessage] = "Unable to load orders! Try again";

                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Creates a view model and pass it to the view
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = $"{AdminRoleName}, {MerchantRoleName}")]
        public async Task<IActionResult> Create(Guid articleId)
        {
            try
            {
                var model = await orderService.CreateAddModelByArticleIdAsync(articleId);

                return View(model);
            }
            catch (ArgumentException ae)
            {
                logger.LogWarning(ae, ae.Message);
                return NotFound();
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                TempData[WarningMessage] = "Problem creating an order! Try again";
                return RedirectToAction("All", "Article");
            }
        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="model">View model with data</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = $"{AdminRoleName}, {MerchantRoleName}")]
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
                await orderService.CreateOrderAsync(model);

                TempData[SuccessMessage] = "Order created successfully.";

                return RedirectToAction("All");
            }
            catch (OrderMachineException ome)
            {
                logger.LogInformation(ome.Message);
                TempData[WarningMessage] = ome.Message;
            }
            catch (ArgumentException ae)
            {
                logger.LogWarning(ae, ae.Message);
                return NotFound();
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                TempData[ErrorMessage] = "Something went wrong trying to create an order! Try again.";
            }
            return RedirectToAction("All", "Article");
        }

        /// <summary>
        /// Changes status of given order
        /// </summary>
        /// <param name="id">Order id</param>
        /// <param name="status">New status</param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = $"{AdminRoleName}, {MerchantRoleName}, {PrinterRoleName}")]
        public async Task<IActionResult> ChangeStatus(Guid id, OrderStatus status, string returnUrl)
        {
            try
            {
                switch (status)
                {
                    case OrderStatus.Printing:
                    case OrderStatus.Completed:
                        if (User.IsInRole(PrinterRoleName) == false)
                        {
                            throw new StatusPermitionException("Not a Printer");
                        }
                        break;
                    case OrderStatus.Waiting:
                    case OrderStatus.NoConsumable:
                    case OrderStatus.Canceled:
                        if (User.IsInRole(AdminRoleName) == false &&
                            User.IsInRole(MerchantRoleName) == false)
                        {
                            throw new StatusPermitionException("Not administrator or merchant");
                        }
                        break;
                    default:
                        throw new StatusPermitionException("Not valid status name");
                }

                await orderService.ChangeStatusAsync(id, status);

                TempData[SuccessMessage] = $"Status changed to {status}";
            }
            catch (StatusPermitionException spe)
            {
                logger.LogWarning(spe.Message);
                TempData[WarningMessage] = "You don't have permission to change to this status";
            }
            catch (StatusException se)
            {
                logger.LogInformation(se.Message);
                TempData[WarningMessage] = se.Message;
            }
            catch (ArgumentException ae)
            {
                logger.LogWarning(ae, ae.Message);
                TempData[WarningMessage] = ae.Message;
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                TempData[WarningMessage] = "Problem occurred! Try again.";
            }

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }
    }
}
