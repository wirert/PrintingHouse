namespace PrintingHouse.Infrastructure.Data.Entities.Enums
{
    /// <summary>
    /// Machine status enumeration 
    /// </summary>
    public enum MachineStatus
    {
        Working,
        WaitForConsumable,
        WaitForMaterial,
        Broken,
        Scrapped
    }
}
