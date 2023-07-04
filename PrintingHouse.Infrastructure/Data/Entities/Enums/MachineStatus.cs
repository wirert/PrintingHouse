namespace PrintingHouse.Infrastructure.Data.Entities.Enums
{
    /// <summary>
    /// Machine status enumeration 
    /// </summary>
    public enum MachineStatus
    {
        Working = 0,
        WaitForConsumable = 1,
        WaitForMaterial = 2,
        Broken = 3,
        Scrapped = 4
    }
}
