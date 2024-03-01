namespace StockManagementSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using StockManagementSystem.Services.Data.Models.Interfaces;
    using StockManagementSystem.Web.Data;
    using StockManagementSystem.Web.ViewModels.Article;

    public class CategoryService : ICategoryService
    {
        private readonly SMSDbContext dbContext;

        public CategoryService(SMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ArticleSelectCategoryFormModel>> GetAllCategoryesAsync()
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

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool result = await this.dbContext
                .Categories
                .AnyAsync(c => c.Id == id);

            return result;
        }
    }
}
