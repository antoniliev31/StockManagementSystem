namespace StockManagementSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StockManagementSystem.Services.Data.Models.Interfaces;
    using StockManagementSystem.Web.ViewModels.Article;

    [Authorize]
    public class ArticleController : Controller
    {
        private readonly ICategoryService categoryService;

        public ArticleController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ArticleFormModel articleModel = new ArticleFormModel()
            {
                Categories = await categoryService.AllCategoryesAsync()
            };

            return View(articleModel);
        }
    }
}
