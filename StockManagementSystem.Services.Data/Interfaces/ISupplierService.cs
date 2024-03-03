namespace StockManagementSystem.Services.Data.Interfaces
{
    using StockManagementSystem.Web.ViewModels.Article;

    public interface ISupplierService
    {
        Task<IEnumerable<string>> GetAllSupplierNamesAsync();
        Task<IEnumerable<ArticleSelectSupplierFormModel>> GetAllSupplierAsync();
        Task<bool> ExistSupplierByNameAsync(Guid supplierId);        
    }
}
