namespace StockManagementSystem.Web.ViewModels.Article
{
    using global::Web.ViewModels.Category;
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Article;

    public class ArticleFormModel
    {
        public ArticleFormModel()
        {
            Categories = new HashSet<ArticleSelectCategoryFormModel>();
        }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Qantity { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public string Supplier { get; set; } = null!;

        public IEnumerable<ArticleSelectCategoryFormModel> Categories { get; set; }

    }
}
