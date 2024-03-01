namespace StockManagementSystem.Web.ViewModels.Category
{
    using System.ComponentModel.DataAnnotations;
    using static StockManagementSystem.Common.EntityValidationConstants.Category;

    public class CategoryFormModel
    {
        [Required]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength)]
        public string Name { get; set; } = null!;
    }
}
