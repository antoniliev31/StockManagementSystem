namespace StockManagementSystem.Services.Data
{
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using StockManagementSystem.Web.Data;
    using Web.ViewModels.Article;

    public class SupplierService : ISupplierService
    {
        private readonly SMSDbContext dbContext;

        public SupplierService(SMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ExistSupplierByNameAsync(Guid supplierId)
        {
            bool result = await this.dbContext
                .Suppliers
                .AnyAsync(s => s.Id == supplierId);

            return result;
        }

        public async Task<IEnumerable<ArticleSelectSupplierFormModel>> GetAllSupplierAsync()
        {
            IEnumerable<ArticleSelectSupplierFormModel> allSupliers = await this.dbContext
                .Suppliers
                .AsNoTracking()
                .Select(x => new ArticleSelectSupplierFormModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToArrayAsync();

            return allSupliers;
        }


    }
}
