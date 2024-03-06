namespace StockManagementSystem.Web.ViewModels.Report
{
    public class ReportViewModel
    {
        public string ArticleNumber { get; set; } = null!;

        public string Title { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Sum => Price * Quantity;

        public string Supplier { get; set; } = null!;

        public string Category { get; set; } = null!;
    }
}
