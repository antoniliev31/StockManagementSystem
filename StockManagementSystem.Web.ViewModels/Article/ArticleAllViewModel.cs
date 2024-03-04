namespace StockManagementSystem.Web.ViewModels.Article
{
    public class ArticleAllViewModel
    {
        public Guid Id { get; set; }
        
        public string ArticleNumber { get; set; } = null!;
        
        public string Title { get; set; } = null!;      
                
        public decimal Price { get; set; }
        
        public int Quantity { get; set; }        
        
        public string Supplier { get; set; } = null!;

        public string Category { get; set; } = null!;
    }
}
