namespace StockManagementSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Category;

    public class Category
    {
        public Category()
        {
            this.ArticlesInThisCategory = new HashSet<Article>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = null!;


        public ICollection<Article> ArticlesInThisCategory { get; set; }
    }
}