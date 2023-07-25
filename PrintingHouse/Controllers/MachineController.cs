namespace PrintingHouse.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using PrintingHouse.Core.Services.Contracts;

    public class MachineController : BaseController
    {
        private readonly IMachineService machineService;

        public MachineController(IMachineService _machineService)
        {
            machineService = _machineService;
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

                return BadRequest(ModelState);
            }

            
        }
    }
}
