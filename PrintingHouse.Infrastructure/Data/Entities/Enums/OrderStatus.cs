namespace PrintingHouse.Infrastructure.Data.Entities.Enums
{
    /// <summary>
    /// Order Status enumeration
    /// </summary>
    public enum OrderStatus
    {
        Printing = 0,
        Waiting = 1,
        NoConsumable = 2,
        Completed = 3,
        Canceled = 4
    }
}
