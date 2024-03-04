namespace StockManagementSystem.Services.Data.Interfaces
{
    using StockManagementSystem.Services.Data.Models.Article;
    using StockManagementSystem.Web.ViewModels.Article;
    using System;

    public interface IArticleService
    {
        Task<AllArticlesFilteredAndPagesServiceModel> AllArticleAsync(AllArticleQueryModel queryModel);
        Task<bool> ArticleExistByIdAsync(Guid id);
        Task<bool> CreateArticleAcync(ArticleFormModel articleFormModel);
        Task<bool> ArticleExistByArticleNumer(string articleNumber);
        Task<ArticleFormModel> GetArticleForEditByIdAsync(Guid id);
        Task<ArticleDetailsViewModel?> GetArticleDetailsByAdAsync(Guid id);
        Task EditArticleByIdAndFormModelAsync(Guid id, ArticleFormModel model);        
        Task<ArticleForDeleteViewModel> GetArticleForDeleteByIdAsync(Guid id);
        Task DeleteArticleByIdAsync(Guid id);
    }
}
