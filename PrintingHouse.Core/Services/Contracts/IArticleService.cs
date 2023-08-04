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
        Task<IEnumerable<AllArticleViewModel>> GetAllAsync(Guid? id);

        /// <summary>
        /// Creates new article
        /// </summary>
        /// <param name="model">Article view model</param>
        /// <exception cref="ArgumentException"></exception>
        Task CreateAsync(ArticleViewModel model);

        /// <summary>
        /// Creates view model for creating a new article and fill it with needed data
        /// </summary>
        /// <param name="materialId"></param>
        /// <param name="colorModelId"></param>
        /// <param name="clientId"></param>
        /// <param name="clientName"></param>
        /// <returns>Article view model</returns>
        /// <exception cref="ArgumentException"></exception>
        Task<ArticleViewModel> GetCreateViewModelWithData(int materialId, int colorModelId, Guid clientId, string? clientName);

        /// <summary>
        /// Finds an article from db and create view model for update
        /// </summary>
        /// <param name="materialId"></param>
        /// <param name="colorModelId"></param>
        /// <param name="articleId"></param>
        /// <returns>Article view model</returns>
        /// <exception cref="ArgumentException"></exception>
        Task<ArticleViewModel> GetEditViewModelWithData(int? materialId, int? colorModelId, Guid articleId);

        /// <summary>
        /// Fill data for select material and color model view model
        /// </summary>        
        /// <param name="clientId"></param>
        /// <param name="articleId"></param>
        /// <returns>Choose article material and color model view model</returns>
        /// <exception cref="ArgumentException"></exception>
        Task<ChooseArticleMaterialAndColorsViewModel> GetSelectVeiwModelWithDataAsync(Guid clientId, Guid? articleId);

        /// <summary>
        /// Check existence of article by id and is it active
        /// </summary>
        /// <param name="id">article id</param>
        /// <returns>Boolean</returns>
        Task<bool> ExistByIdAsync(Guid? id);        

        /// <summary>
        /// Edit existing article
        /// </summary>
        /// <param name="model">Article view model with data changes</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">thrown when Article is null or IsActive is false, or ClientId is different</exception>
        Task EditAsync(ArticleViewModel model);

        /// <summary>
        /// Soft delete article
        /// </summary>
        /// <param name="id">Article id</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task DeleteByIdAsync(Guid id);

        /// <summary>
        /// Get design name by article id
        /// </summary>
        /// <param name="id">Article identifier</param>
        /// <returns>Design name</returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task<string> GetFileNameByIdAsync(Guid id);
    }
}
