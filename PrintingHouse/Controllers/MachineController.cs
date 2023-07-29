namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Core.Exceptions;
    using Core.Services.Contracts;
    using static Core.Constants.MessageConstants;
    using static Core.Constants.RoleNamesConstants;
    using Infrastructure.Data.Entities.Enums;
    using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = $"{Admin}, {Merchant}")]
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
        [Authorize(Roles = $"{Admin}, {Merchant}, {Printer}")]
        public async Task<IActionResult> ChangeStatus(int id, OrderStatus status)
        {
            try
            {
                switch (status)
                {
                    case OrderStatus.Printing:
                    case OrderStatus.Completed:
                        if (User.IsInRole(Printer) == false)
                        {
                            throw new StatusPermitionException();
                        }
                        break;
                    case OrderStatus.Waiting:
                    case OrderStatus.NoConsumable:
                    case OrderStatus.Canceled:
                        if (User.IsInRole(Admin) == false &&
                            User.IsInRole(Merchant) == false)
                        {
                            throw new StatusPermitionException();
                        }
                        break;
                    default:
                        throw new StatusPermitionException();
                }

                await orderService.ChangeStatusAsync(id, status);

                TempData[SuccessMessage] = $"Status changed to {status}";
            }
            catch (StatusPermitionException)
            {
                TempData[WarningMessage] = "You don't have permission to change to this status";
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
