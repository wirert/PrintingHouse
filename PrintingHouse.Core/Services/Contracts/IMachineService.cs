namespace PrintingHouse.Core.Services.Contracts
{
    using PrintingHouse.Core.Models.Machine;

    public interface IMachineService
    {
        Task<IEnumerable<MachineSelectViewModel>> GetMachinesIdsAsync();

        Task<MachineOrderViewModel> GetMachineOrdersAsync(int machineId);
    }
}
