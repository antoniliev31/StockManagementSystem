using StockManagementSystem.Web.ViewModels.Article;

namespace StockManagementSystem.Services.Data.Interfaces
{
    public interface IArticleService
    {
        Task<bool> CreateArticleAcync(ArticleFormModel articleFormModel);
        Task<bool> ExistArticleByArticleNumer(string articleNumber);
    }
}
