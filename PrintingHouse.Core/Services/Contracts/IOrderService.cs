namespace PrintingHouse.Core.Services.Contracts
{
    using PrintingHouse.Core.Models.Order;

    public interface IOrderService
    {
        Task<AddOrderViewModel> CreateAddModelByArticleIdAsync(Guid articleId);

        Task CreateOrder(AddOrderViewModel model);

        Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync();
    }
}
