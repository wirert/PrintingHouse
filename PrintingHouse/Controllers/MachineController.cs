namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Core.Services.Contracts;
    using static Core.Constants.MessageConstants;
    using static Core.Constants.ApplicationConstants;
    using Infrastructure.Data.Entities.Enums;

    public class MachineController : BaseController
    {
        private readonly IMachineService machineService;
        private readonly IOrderService orderService;

        public MachineController(IMachineService _machineService,
            IOrderService _orderService)
        {
            machineService = _machineService;
            orderService = _orderService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var machines = await machineService.GetMachinesIdsAsync();

                ViewBag.Machines = new SelectList(machines.OrderBy(m => m.Id), "Id", "Name");

                return View();
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
                return BadRequest("No order found");
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
                    throw new Exception();
                }

                await machineService.MoveOrderInFrontAsync(id);

                TempData[SuccessMessage] = "The order is moved in front of athors";
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Unable to change possition of this order";
            }

            return RedirectToAction("Index");
        }
    }
}
