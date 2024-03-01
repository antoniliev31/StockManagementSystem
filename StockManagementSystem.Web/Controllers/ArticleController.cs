namespace StockManagementSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StockManagementSystem.Services.Data.Interfaces;
    using StockManagementSystem.Services.Data.Models.Interfaces;
    using StockManagementSystem.Web.ViewModels.Article;

    using static StockManagementSystem.Common.NotificationMessagesConstants;

    [Authorize]
    public class ArticleController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly ISupplierService supplierService;
        private readonly IArticleService articleService;

        public ArticleController(ICategoryService categoryService, ISupplierService supplierService, IArticleService articleService)
        {
            this.categoryService = categoryService;
            this.supplierService = supplierService;
            this.articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ArticleFormModel articleModel = new ArticleFormModel()
            {
                Categories = await categoryService.GetAllCategoryesAsync(),
                Suppliers = await supplierService.GetAllSupplierAsync(),
            };

            return View(articleModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ArticleFormModel articleFormModel)
        {
            bool categoryExist = await this.categoryService.ExistsByIdAsync(articleFormModel.CategoryId);

            if (!categoryExist)
            {
                this.ModelState.AddModelError(nameof(articleFormModel.CategoryId), "Category does not exist!");
            }            

            bool supplierExist = await this.supplierService.ExistSupplierByNameAsync(articleFormModel.SupplierId);

            if (!supplierExist)
            {
                this.ModelState.AddModelError(nameof(articleFormModel.SupplierId.ToString), "Supplier does not exist!");
            }

            bool articleNumberExist = await this.articleService.ExistArticleByArticleNumer(articleFormModel.ArticleNumber);

            if (articleNumberExist) 
            {
                articleFormModel.Categories = await this.categoryService.GetAllCategoryesAsync();
                articleFormModel.Suppliers = await this.supplierService.GetAllSupplierAsync();

                this.TempData[ErrorMessage] = "Article with this number already exists!";

                return this.View(articleFormModel);
            }

            if (!this.ModelState.IsValid)
            {
                articleFormModel.Categories = await this.categoryService.GetAllCategoryesAsync();
                articleFormModel.Suppliers = await this.supplierService.GetAllSupplierAsync();

                return this.View(articleFormModel);
            }

            try
            {
                await this.articleService.CreateArticleAcync(articleFormModel);
                this.TempData[SuccessMessage] = "Article was created successfully!";

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new article! Please try again later or contact administrator!");
                articleFormModel.Categories = await this.categoryService.GetAllCategoryesAsync();
                articleFormModel.Suppliers = await this.supplierService.GetAllSupplierAsync();

                return this.View(articleFormModel);
            }
        }
    }
}
