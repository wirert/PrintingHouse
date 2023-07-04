namespace PrintingHouse.Infrastructure.Data.Entities.Enums
{
    /// <summary>
    /// Order Status enumeration
    /// </summary>
    public enum OrderStatus
    {
        Waiting = 0,
        InProgress = 1,
        Completed = 2,
        NoConsumable = 3,
        Canceled = 4,
        Problem = 5
    }
}
