namespace StockManagementSystem.Web.ViewModels.Supplier
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Spplier;

    public class SupplierFormModel
    {
        [Required]
        [StringLength(SpplierNameMaxLength, MinimumLength = SpplierNameMinLength)]
        public string Title { get; set; } = null!;
    }
}
