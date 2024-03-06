namespace StockManagementSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;    
    using StockManagementSystem.Services.Data.Interfaces;
    using StockManagementSystem.Web.ViewModels.Report;

    public class ReportController : Controller
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }


        public async Task<IActionResult> Balance()
        {
            IEnumerable<ReportViewModel> reportViewModels = await this.reportService.GetBalance();

            decimal totalSum = reportViewModels.Sum(model => model.Sum);

            BalanceViewModel balanceViewModel = new BalanceViewModel
            {
                ReportViewModels = reportViewModels,
                TotalSum = totalSum
            };

            return View(balanceViewModel);
        }
    }
}



