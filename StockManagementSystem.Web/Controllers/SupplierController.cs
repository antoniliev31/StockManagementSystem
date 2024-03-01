namespace StockManagementSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;    
    using Microsoft.AspNetCore.Authorization;
    using StockManagementSystem.Web.ViewModels.Supplier;
    using StockManagementSystem.Data.Models;
    using StockManagementSystem.Web.Data;

    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class SupplierController : Controller
    {
        private readonly SMSDbContext dbContext;

        public SupplierController(SMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            SupplierFormModel suplierModel = new SupplierFormModel();

            return View(suplierModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SupplierFormModel suplierModel)
        {
            if (!ModelState.IsValid)
            {
                return View(suplierModel);
            }

            var suoolierToAdd = new Supplier()
            {
                Name = suplierModel.Title
            };

            try
            {
                await dbContext.AddAsync(suoolierToAdd);
                await dbContext.SaveChangesAsync();
                this.TempData[SuccessMessage] = "Supplier aded successful!";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new supplier! Please try again later or contact administrator!");
            }            

            return RedirectToAction("Index", "Home");
        }
    }
}
