namespace PrintingHouse.Core.Contracts
{
    using PrintingHouse.Core.Models.Article;

    public interface IColorModelService
    {
        Task<ICollection<AddArticleColorVeiwModel>?> GetColorModelColorsAsync(int ColorModelId);
    }
}
