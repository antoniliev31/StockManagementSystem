namespace StockManagementSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ArticleController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
