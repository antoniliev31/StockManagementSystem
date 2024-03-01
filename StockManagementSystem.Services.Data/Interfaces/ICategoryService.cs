using Web.ViewModels.Category;

namespace StockManagementSystem.Services.Data.Models.Interfaces
{
        
    public interface ICategoryService
    {
        Task<bool> ExistByNameAsync(string name);
        Task<IEnumerable<ArticleSelectCategoryFormModel>> AllCategoryesAsync();
    }
}
