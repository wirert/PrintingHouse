namespace PrintingHouse.Core.Services.Contracts
{
    using PrintingHouse.Core.Models.Machine;

    public interface IMachineService
    {
        /// <summary>
        /// Gets all machines for select
        /// </summary>
        /// <returns>Machine select view model</returns>
        Task<IEnumerable<MachineSelectViewModel>> GetMachinesIdsAsync();

        /// <summary>
        /// Gets orders for a particular machine
        /// </summary>
        /// <param name="machineId">machine identifier</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<MachineOrderViewModel> GetMachineOrdersAsync(int machineId);

        /// <summary>
        /// Make order first in queue for printing
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task MoveOrderInFrontAsync(Guid orderId);
    }
}
