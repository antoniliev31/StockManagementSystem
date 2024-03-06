namespace StockManagementSystem.Web.ViewModels.Report
{
    public class BalanceViewModel
    {
        public IEnumerable<ReportViewModel> ReportViewModels { get; set; } = null!;

        public decimal TotalSum { get; set; }
    }
}
