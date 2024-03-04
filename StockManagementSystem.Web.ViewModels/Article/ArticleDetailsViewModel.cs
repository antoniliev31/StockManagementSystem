namespace StockManagementSystem.Web.ViewModels.Article
{
    public class ArticleDetailsViewModel : ArticleAllViewModel
    {
        public string? Description { get; set; }

        public string CreatedOn { get; set; } = null!;

    }
}
