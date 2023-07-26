namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using PrintingHouse.Core.Services;
    using PrintingHouse.Core.Services.Contracts;
    using PrintingHouse.Infrastructure.Data.Entities.Enums;
    using static Core.Constants.MessageConstants;

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
                var machines =  await machineService.GetMachinesIdsAsync();

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
        public async Task<IActionResult> MoveInFront(int id, OrderStatus status)
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

            return RedirectToAction("Index");
        }
    }
}
