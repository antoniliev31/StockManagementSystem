using StockManagementSystem.Web.ViewModels.Article;

namespace StockManagementSystem.Services.Data.Models.Interfaces
{

    public interface ICategoryService
    {
        Task<bool> ExistByNameAsync(string name);

        Task<bool> ExistsByIdAsync(int id);

        Task<IEnumerable<ArticleSelectCategoryFormModel>> GetAllCategoryesAsync();
    }
}
