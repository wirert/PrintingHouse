namespace PrintingHouse.Core.Services.Contracts
{
    using Models.Article;

    /// <summary>
    /// Article service interface for IoC
    /// </summary>
    public interface IArticleService
    {
        /// <summary>
        /// Get all articles or all articles of certain client by client id
        /// </summary>
        /// <param name="id">client id (nullable)</param>
        /// <returns>Enumeration of All article view model</returns>
        Task<IEnumerable<AllArticleViewModel>> GetAllAsync(int? id);

        /// <summary>
        /// Creates new article
        /// </summary>
        /// <param name="model">Article view model</param>
        Task CreateAsync(ArticleViewModel model);

        /// <summary>
        /// Fill data for select material and color model view model
        /// </summary>
        /// <param name="model">Choose article material and color model view model</param>
        /// <returns>Choose article material and color model view model</returns>
        /// <exception cref="Exception">Thrown when clinet is null or client isActive is false</exception>
        Task<ChooseArticleMaterialAndColorsViewModel> FillSelectModelWithDataAsync(ChooseArticleMaterialAndColorsViewModel model);

        /// <summary>
        /// Check existence of article by id and is it active
        /// </summary>
        /// <param name="id">article id</param>
        /// <returns>Boolean</returns>
        Task<bool> ExistByIdAsync(Guid? id);

        /// <summary>
        /// Get article by id and whether it is active
        /// </summary>
        /// <param name="id">Guid article id</param>
        /// <returns>Article view model</returns>
        /// <exception cref="ArgumentNullException">Thrown when Article is null</exception>
        Task<ArticleViewModel> GetByIdAsync(Guid id);

        /// <summary>
        /// Edit existing article
        /// </summary>
        /// <param name="model">Article view model with data changes</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">thrown when Article is null or IsActive is false, or ClientId is different</exception>
        Task EditAsync(ArticleViewModel model);
    }
}
