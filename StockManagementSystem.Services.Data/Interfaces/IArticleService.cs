namespace StockManagementSystem.Services.Data.Interfaces
{
    using StockManagementSystem.Services.Data.Models.Article;
    using StockManagementSystem.Web.ViewModels.Article;

    public interface IArticleService
    {
        Task<AllArticlesFilteredAndPagesServiceModel> AllArticleAsync(AllArticleQueryModel queryModel);
        Task<bool> CreateArticleAcync(ArticleFormModel articleFormModel);
        Task<bool> ExistArticleByArticleNumer(string articleNumber);
    }
}
