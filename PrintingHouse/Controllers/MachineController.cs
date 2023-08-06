namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Core.Services.Contracts;
    using static Core.Constants.MessageConstants;
    using static Core.Constants.ApplicationConstants;
    using Infrastructure.Data.Entities.Enums;
    using Microsoft.Extensions.Caching.Memory;
    using PrintingHouse.Core.Models.Machine;

    public class MachineController : BaseController
    {
        private readonly IMachineService machineService;
        private readonly IOrderService orderService;
        private readonly ILogger logger;
        private readonly IMemoryCache cache;

        public MachineController(
            IMachineService _machineService,
            IOrderService _orderService,
            ILogger<MachineController> _logger,
            IMemoryCache _cache)
        {
            machineService = _machineService;
            orderService = _orderService;
            logger = _logger;
            cache = _cache;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var machines = cache.Get<IEnumerable<MachineSelectViewModel>>(MachinesCacheKey);

                if (machines == null)
                {
                    machines = await machineService.GetMachinesIdsAsync();

                    var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(MachinesCacheExpirationHours));

                    cache.Set(MachinesCacheKey, machines, cacheOptions);
                }

                ViewBag.Machines = new SelectList(machines.OrderBy(m => m.Id), "Id", "Name");

                return View();
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                TempData[WarningMessage] = "Error loading machines";

                return RedirectToAction("All", "Order");
            }
        }

        public async Task<IActionResult> GetOrders(int id)
        {
            try
            {
                var model = await machineService.GetMachineOrdersAsync(id);

                return PartialView("_MachineOrdersPartial", model);
            }
            catch (ArgumentException ae)
            {
                logger.LogInformation(ae, ae.Message);
                return NotFound();
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [Authorize(Roles = $"{AdminRoleName}, {MerchantRoleName}")]
        public async Task<IActionResult> MoveInFront(Guid id, OrderStatus status)
        {
            try
            {
                if (status != OrderStatus.Waiting && status != OrderStatus.NoConsumable)
                {
                    throw new ArgumentException("Status of order is different of 'Waiting' or 'NoConsumable'");
                }

                await machineService.MoveOrderInFrontAsync(id);

                TempData[SuccessMessage] = "The order is moved in front of athors";
            }
            catch (ArgumentException ae)
            {
                logger.LogInformation(ae, ae.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                TempData[ErrorMessage] = "Unable to change possition of this order";
            }

            return RedirectToAction("Index");
        }
    }
}
