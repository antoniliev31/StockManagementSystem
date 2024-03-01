namespace StockManagementSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using StockManagementSystem.Data.Models;
    using StockManagementSystem.Services.Data.Interfaces;
    using StockManagementSystem.Web.Data;
    using StockManagementSystem.Web.ViewModels.Article;

    public class ArticleService : IArticleService
    {
        private readonly SMSDbContext dbContext;

        public ArticleService(SMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateArticleAcync(ArticleFormModel articleFormModel)
        {
            Article newArticle = new Article
            {
                ArticleNumber = articleFormModel.ArticleNumber,
                Title = articleFormModel.Title,
                Description = articleFormModel.Description,
                Price = articleFormModel.Price,
                Qantity = articleFormModel.Qantity,
                SupplierId = articleFormModel.SupplierId,
                CategoryId = articleFormModel.CategoryId,
            };

            await this.dbContext.Articles.AddAsync(newArticle);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistArticleByArticleNumer(string articleNumber)
        {
            bool result = await this.dbContext
                .Articles
                .AnyAsync(a => a.ArticleNumber == articleNumber);

            return result;
        }
    }
}
