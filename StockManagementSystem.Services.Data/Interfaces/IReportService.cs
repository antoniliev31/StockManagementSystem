namespace StockManagementSystem.Services.Data.Interfaces
{
    using StockManagementSystem.Web.ViewModels.Report;

    public interface IReportService
    {
        Task<IEnumerable<ReportViewModel>> GetBalance();        
    }
}
