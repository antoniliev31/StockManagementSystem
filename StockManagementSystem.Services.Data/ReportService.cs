namespace StockManagementSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;    
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StockManagementSystem.Services.Data.Interfaces;
    using StockManagementSystem.Web.Data;
    using StockManagementSystem.Web.ViewModels.Report;

    public class ReportService : IReportService
    {
        private readonly SMSDbContext dbContext;

        public ReportService(SMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ReportViewModel>> GetBalance()
        {
            var allArticles = await this.dbContext
                .Articles
                .Include(a => a.Category)
                .Include(a => a.Supplier)
                .Where(a => a.Quantity > 0)
                .Select(a => new ReportViewModel
                {
                    ArticleNumber = a.ArticleNumber,
                    Title = a.Title,
                    Price = a.Price,
                    Quantity = a.Quantity,
                    Supplier = a.Supplier.Name,
                    Category = a.Category.Name
                })
                .ToArrayAsync();

            return allArticles;          
            
        }
    }
}
