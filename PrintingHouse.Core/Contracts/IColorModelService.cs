namespace PrintingHouse.Core.Contracts
{
    using PrintingHouse.Core.Models.Article;

    public interface IColorModelService
    {
        Task<IList<AddArticleColorVeiwModel>> GetColorModelColorsAsync(int ColorModelId);
    }
}
