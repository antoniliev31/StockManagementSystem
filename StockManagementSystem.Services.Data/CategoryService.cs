namespace StockManagementSystem.Services.Data
{
    using global::Web.ViewModels.Category;
    using Microsoft.EntityFrameworkCore;
    using StockManagementSystem.Services.Data.Models.Interfaces;
    using StockManagementSystem.Web.Data;    

    public class CategoryService : ICategoryService
    {
        private readonly SMSDbContext dbContext;

        public CategoryService(SMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ArticleSelectCategoryFormModel>> AllCategoryesAsync()
        {
            IEnumerable<ArticleSelectCategoryFormModel> allCategories = await this.dbContext
                .Categories
                .AsNoTracking()
                .Select(c =>  new ArticleSelectCategoryFormModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

            return allCategories;
        }

        public async Task<bool> ExistByNameAsync(string name)
        {
            bool result = await this.dbContext
                .Categories
                .AnyAsync(c => c.Name == name);

            return result;
        }
    }
}
