namespace StockManagementSystem.Web.ViewModels.Article
{
    public class ArticleForDeleteViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string ArticleNumber { get; set; } = null!;        

        public string Supplier { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
