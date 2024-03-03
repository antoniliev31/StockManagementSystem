namespace StockManagementSystem.Services.Data.Models.Article
{
    using Web.ViewModels.Article;

    public class AllArticlesFilteredAndPagesServiceModel
    {
        public AllArticlesFilteredAndPagesServiceModel()
        {
            this.Articles = new HashSet<ArticleAllViewModel>();
        }

        public int TotalArticlesCount { get; set; }

        public IEnumerable<ArticleAllViewModel> Articles { get; set; }
    }
}
