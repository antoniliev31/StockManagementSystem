namespace StockManagementSystem.Web.ViewModels.Article
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Article;

    public class ArticleFormModel
    {
        public ArticleFormModel()
        {
            Categories = new HashSet<ArticleSelectCategoryFormModel>();
            Suppliers = new HashSet<ArticleSelectSupplierFormModel>();
        }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        [Display(Name = "Article name")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ArticleNumberMaxLength, MinimumLength = ArticleNumberMinLength)]
        [Display(Name = "Article number")]
        public string ArticleNumber { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int Qantity { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public Guid SupplierId { get; set; }

        public IEnumerable<ArticleSelectCategoryFormModel> Categories { get; set; }
        public IEnumerable<ArticleSelectSupplierFormModel> Suppliers { get; set; }

    }
}
