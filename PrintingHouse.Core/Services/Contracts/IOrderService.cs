namespace PrintingHouse.Core.Services.Contracts
{
    using PrintingHouse.Core.Models.Order;
    using PrintingHouse.Infrastructure.Data.Entities.Enums;

    public interface IOrderService
    {
        Task<AddOrderViewModel> CreateAddModelByArticleIdAsync(Guid articleId);

        Task CreateOrder(AddOrderViewModel model);

        Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync();

        Task ChangeStatusAsync(Guid id, OrderStatus status);

        /// <summary>
        /// Changes expected print time and expected print date of all orders with given material and color model.
        /// Respectivly may change machine id (if there are more than one with same parameters).
        /// If passed an order id, it sets it in front of the queue of machines
        /// </summary>
        /// <param name="materialId"></param>
        /// <param name="colorModelId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task RearangeAllOrderOfParticularTypeAsync(int materialId, int colorModelId, Guid? orderId = null);
    }
}
