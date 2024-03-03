namespace StockManagementSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using StockManagementSystem.Data.Models;
    using StockManagementSystem.Services.Data.Interfaces;
    using StockManagementSystem.Services.Data.Models.Article;
    using StockManagementSystem.Web.Data;
    using StockManagementSystem.Web.ViewModels.Article;
    using StockManagementSystem.Web.ViewModels.Article.Enums;

    public class ArticleService : IArticleService
    {
        private readonly SMSDbContext dbContext;

        public ArticleService(SMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AllArticlesFilteredAndPagesServiceModel> AllArticleAsync(AllArticleQueryModel queryModel)
        {
            IQueryable<Article> articlesQuery = dbContext
                .Articles
                .Where(a => a.Qantity > 0)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                articlesQuery = articlesQuery
                    .Where(a => a.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                articlesQuery = articlesQuery
                    .Where(a => EF.Functions.Like(a.Title, wildCard) ||
                                EF.Functions.Like(a.Supplier.Name, wildCard) ||
                                EF.Functions.Like(a.ArticleNumber, wildCard));                              
            }

            articlesQuery = queryModel.ArticleSorting switch
            {
                ArticleSorting.PriceAscending => articlesQuery
                .OrderBy(a => a.Price),
                ArticleSorting.PriceDescending => articlesQuery
                  .OrderByDescending(a => a.Price),
                ArticleSorting.CreatedOnAscending => articlesQuery
                  .OrderByDescending(a => a.CreatedOn),
                ArticleSorting.CreatedOnDescending => articlesQuery
                  .OrderBy (a => a.CreatedOn),
                _ => articlesQuery
                .OrderBy(a => a.Price)
            };

            IEnumerable<ArticleAllViewModel> allArticles = await articlesQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.ArticlesPerPage)
                .Take(queryModel.ArticlesPerPage)
                .Select(a => new ArticleAllViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    ArticleNumber = a.ArticleNumber,
                    Description = a.Description,
                    Price = a.Price,
                    Quantity = a.Qantity,
                    Supplier = a.Supplier.Name,
                    Category = a.Category.Name
                })
                .ToArrayAsync();

            int totalArticle = articlesQuery.Count();

            return new AllArticlesFilteredAndPagesServiceModel
            {
                TotalArticlesCount = totalArticle,
                Articles = allArticles
            };
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
