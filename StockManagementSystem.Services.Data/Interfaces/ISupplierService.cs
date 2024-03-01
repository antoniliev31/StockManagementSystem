using StockManagementSystem.Web.ViewModels.Article;

namespace StockManagementSystem.Services.Data.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<ArticleSelectSupplierFormModel>> GetAllSupplierAsync();

        Task<bool> ExistSupplierByNameAsync(Guid supplierId);        
    }
}
