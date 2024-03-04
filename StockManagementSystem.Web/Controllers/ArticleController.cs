namespace StockManagementSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StockManagementSystem.Services.Data.Interfaces;
    using StockManagementSystem.Services.Data.Models.Article;
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
        public async Task<IActionResult> All([FromQuery] AllArticleQueryModel queryModel)
        {
            AllArticlesFilteredAndPagesServiceModel serviceModel = await this.articleService.AllArticleAsync(queryModel);

            queryModel.Articles = serviceModel.Articles;
            queryModel.TotalArticles = serviceModel.TotalArticlesCount;
            queryModel.Categories = await this.categoryService.AllCategoryNamesAsync();
            queryModel.Suppliers = await this.supplierService.GetAllSupplierNamesAsync();

            return this.View(queryModel);
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

            bool articleNumberExist = await this.articleService.ArticleExistByArticleNumer(articleFormModel.ArticleNumber);

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

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            bool articleExist = await this.articleService.ArticleExistByIdAsync(id);

            if (!articleExist)
            {
                this.TempData[ErrorMessage] = "Item with this id does not exist!";
                return this.RedirectToAction("All", "Article");
            }

            try
            {
                ArticleDetailsViewModel? viewModel = await this.articleService.GetArticleDetailsByAdAsync(id);

                return this.View(viewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occurred! Please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            bool articleExist = await this.articleService.ArticleExistByIdAsync(id);

            if (!articleExist)
            {
                this.TempData[ErrorMessage] = "Item with this id does not exist!";
                return this.RedirectToAction("All", "Article");
            }

            try
            {
                ArticleFormModel formModel = await this.articleService.GetArticleForEditByIdAsync(id);

                formModel.Categories = await this.categoryService.GetAllCategoryesAsync();
                formModel.Suppliers = await this.supplierService.GetAllSupplierAsync();

                return this.View(formModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occurred! Please try again later!";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ArticleFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.categoryService.GetAllCategoryesAsync();
                model.Suppliers = await this.supplierService.GetAllSupplierAsync();
                return this.View(model);
            }

            bool articleExist = await this.articleService.ArticleExistByIdAsync(id);

            if (!articleExist)
            {
                this.TempData[ErrorMessage] = "Item with this id does not exist!";
                return this.RedirectToAction("All", "Article");
            }           


            try
            {
                await this.articleService.EditArticleByIdAndFormModelAsync(id, model);
                model.Categories = await this.categoryService.GetAllCategoryesAsync();
                model.Suppliers = await this.supplierService.GetAllSupplierAsync();
                this.TempData[SuccessMessage] = "You have successfully changed the details!";
                return this.RedirectToAction("Details", "Article", new { id = id });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error! Please try again later.");
            }

            return this.RedirectToAction("Details", "Article", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete (Guid id)
        {
            bool articleExist = await this.articleService.ArticleExistByIdAsync(id);

            if (!articleExist)
            {
                this.TempData[ErrorMessage] = "Item with this id does not exist!";
                return this.RedirectToAction("All", "Article");
            }

            try
            {
                ArticleForDeleteViewModel viewModel = await this.articleService.GetArticleForDeleteByIdAsync(id);

                return this.View(viewModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occurred! Please try again later!";
                return this.RedirectToAction("Details", "Article", new { id = id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, ArticleForDeleteViewModel model)
        {
            bool articleExist = await this.articleService.ArticleExistByIdAsync(id);

            if (!articleExist)
            {
                this.TempData[ErrorMessage] = "Item with this id does not exist!";
                return this.RedirectToAction("All", "Article");
            }

            try
            {
                await this.articleService.DeleteArticleByIdAsync(id);

                this.TempData[SuccessMessage] = "Successfully deleted item!";
                return this.RedirectToAction("All", "Article");
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occurred! Please try again later!";
                return this.RedirectToAction("Delete", "Artticle", new { id = id });
            }
        }
    }
}
