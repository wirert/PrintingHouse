namespace PrintingHouse.Core.Services.Contracts
{
    using Exceptions;
    using Models.Order;
    using Infrastructure.Data.Entities.Enums;

    public interface IOrderService
    {
        /// <summary>
        /// Creates a View model for Create a new order for a article
        /// </summary>
        /// <param name="articleId">article identifier</param>
        /// <returns>Add order view model</returns>
        /// <exception cref="ArgumentException"></exception>
        Task<AddOrderViewModel> CreateAddModelByArticleIdAsync(Guid articleId);

        /// <summary>
        /// Creates new order
        /// </summary>
        /// <param name="model">View model</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="OrderMachineException"></exception>
        Task CreateOrderAsync(AddOrderViewModel model);

        /// <summary>
        /// Gets all orders
        /// </summary>
        /// <returns>Enumeration of Order View model</returns>
        Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync();

        /// <summary>
        /// Make order first in queue for printing
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task MoveOrderInFrontAsync(Guid orderId, OrderStatus status);

        /// <summary>
        /// Changes status of an order
        /// </summary>
        /// <param name="id">Order identifier</param>
        /// <param name="status">The new Status of order</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="StatusException"></exception>
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
