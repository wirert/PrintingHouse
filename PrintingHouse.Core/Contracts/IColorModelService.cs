namespace PrintingHouse.Core.Contracts
{
    using PrintingHouse.Core.Models.Article;
    using PrintingHouse.Core.Models.ColorModel;

    public interface IColorModelService
    {
        Task<IList<AddArticleColorVeiwModel>> GetColorModelColorsAsync(int colorModelId);

        Task<ICollection<ColorModelSelectViewModel>> GetColorModelByMaterialIdAsync(string materialId);

        Task<bool> ExistByIdAsync(int colorModelId);
    }
}
