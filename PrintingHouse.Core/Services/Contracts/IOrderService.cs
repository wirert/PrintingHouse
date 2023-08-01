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

        Task RearangeAllOrderOfParticularTypeAsync(int materialId, int colorModelId, Guid? orderId = null);
    }
}
