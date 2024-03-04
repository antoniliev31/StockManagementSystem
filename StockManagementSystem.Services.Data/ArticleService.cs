namespace StockManagementSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using StockManagementSystem.Data.Models;
    using StockManagementSystem.Services.Data.Interfaces;
    using StockManagementSystem.Services.Data.Models.Article;
    using StockManagementSystem.Web.Data;
    using StockManagementSystem.Web.ViewModels.Article;
    using StockManagementSystem.Web.ViewModels.Article.Enums;
    using System;
    using System.Linq;

    public class ArticleService : IArticleService
    {
        private readonly SMSDbContext dbContext;

        public ArticleService(SMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ArticleExistByIdAsync(Guid id)
        {
            bool result = await this.dbContext
                .Articles
                .AnyAsync(a => a.Id == id);

            return result;
        }

        public async Task<bool> ArticleExistByArticleNumer(string articleNumber)
        {
            bool result = await this.dbContext
                .Articles
                .AnyAsync(a => a.ArticleNumber == articleNumber);

            return result;
        }

        public async Task<AllArticlesFilteredAndPagesServiceModel> AllArticleAsync(AllArticleQueryModel queryModel)
        {
            IQueryable<Article> articlesQuery = dbContext
                .Articles 
                .Where(a => a.Price >=0)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                articlesQuery = articlesQuery
                    .Where(a => a.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.Supplier))
            {
                articlesQuery = articlesQuery
                    .Where(a => a.Supplier.Name == queryModel.Supplier);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                articlesQuery = articlesQuery
                    .Where(a => EF.Functions.Like(a.Title, wildCard) ||                                
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

            articlesQuery = articlesQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.ArticlesPerPage)
                .Take(queryModel.ArticlesPerPage);

            IEnumerable<ArticleAllViewModel> allArticles = await articlesQuery
                .Select(a => new ArticleAllViewModel
                {
                    Id = a.Id,
                    Title = a.Title,                                        
                    Price = a.Price,
                    Quantity = a.Quantity,                    
                    Category = a.Category.Name
                })
                .ToArrayAsync();

            int totalArticle = dbContext.Articles.Count();

            return new AllArticlesFilteredAndPagesServiceModel
            {
                TotalArticlesCount = totalArticle,
                Articles = allArticles,
                CurrentPage = queryModel.CurrentPage,
                ArticlesPerPage = queryModel.ArticlesPerPage
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
                Quantity = articleFormModel.Qantity,
                SupplierId = articleFormModel.SupplierId,
                CategoryId = articleFormModel.CategoryId,
            };

            await this.dbContext.Articles.AddAsync(newArticle);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<ArticleDetailsViewModel?> GetArticleDetailsByAdAsync(Guid id)
        {
            Article article = await this.dbContext
                .Articles
                .Include(a => a.Category)
                .Include(a => a.Supplier)
                .FirstAsync(a => a.Id == id);

            var viewModel = new ArticleDetailsViewModel
            {
                Id = article.Id,
                Title = article.Title,
                ArticleNumber = article.ArticleNumber,
                Description = article.Description,
                Price = article.Price,
                Category = article.Category.Name,
                Supplier = article.Supplier.Name,
                Quantity = article.Quantity,
                CreatedOn = article.CreatedOn.ToString("dd/MM/yyyy H:mm"),
            };

            return viewModel;
        }

        public async Task<ArticleFormModel> GetArticleForEditByIdAsync(Guid id)
        {
            Article article = await this.dbContext
                .Articles
                .Include(a => a.Category)
                .Include(a => a.Supplier)
                .FirstAsync (a => a.Id == id);

            return new ArticleFormModel
            {
                Title = article.Title,
                ArticleNumber = article.ArticleNumber,
                Description = article.Description,
                Price = article.Price,
                Qantity = article.Quantity,
                CategoryId = article.CategoryId,
                SupplierId = article.SupplierId
            };
        }

        public async Task EditArticleByIdAndFormModelAsync(Guid id, ArticleFormModel model)
        {
            var article = await this.dbContext
                .Articles
                .Include(a => a.Category)
                .Include(a => a.Supplier)
                .FirstAsync(a => a.Id == id);

            if (article != null)
            {
                article.Title = model.Title;
                article.Description = model.Description;
                article.Price = model.Price;
                article.SupplierId = model.SupplierId;
                article.CategoryId = model.CategoryId;
                article.Quantity = model.Qantity;
                article.ArticleNumber = model.ArticleNumber;

                await this.dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Article not found!");
            }
        }        

        public async Task<ArticleForDeleteViewModel> GetArticleForDeleteByIdAsync(Guid id)
        {
            var article = await this.dbContext
                .Articles
                .Include(a => a.Category)
                .Include(a => a.Supplier)
                .FirstAsync(a => a.Id == id);

            return new ArticleForDeleteViewModel
            {
                Id = article.Id,
                Title = article.Title,
                ArticleNumber = article.ArticleNumber,
                Supplier = article.Supplier.Name,
                Category = article.Category.Name,
                Quantity = article.Quantity
            };
        }

        public async Task DeleteArticleByIdAsync(Guid id)
        {
            var article = await this.dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);

            if (article != null)
            {
                this.dbContext.Articles.Remove(article);
                await this.dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Article with id {id} does not exist.");
            }
        }
        
    }
}
