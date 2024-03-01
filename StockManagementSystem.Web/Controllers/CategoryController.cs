namespace StockManagementSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StockManagementSystem.Data.Models;
    using StockManagementSystem.Services.Data.Models.Interfaces;
    using StockManagementSystem.Web.Data;
    using StockManagementSystem.Web.ViewModels.Category;

    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class CategoryController : Controller
    {
        private readonly SMSDbContext dbContext;
        private readonly ICategoryService categoryService;

        public CategoryController(SMSDbContext dbContext, ICategoryService categoryService)
        {
            this.dbContext = dbContext;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            CategoryFormModel categoryModel = new CategoryFormModel();

            return View(categoryModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryFormModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryModel);
            }

            bool categoryExist = await this.categoryService.ExistByNameAsync(categoryModel.Name);

            if (categoryExist)
            {
                this.TempData[ErrorMessage] = "Category already exists!";
                return View(categoryModel);
            }

            var categoryToAdd = new Category()
            {
                Name = categoryModel.Name
            };

            try
            {
                await dbContext.AddAsync(categoryToAdd);
                await dbContext.SaveChangesAsync();
                this.TempData[SuccessMessage] = "Category aded successful!";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new category! Please try again later or contact administrator!");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
