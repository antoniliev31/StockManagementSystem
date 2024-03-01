namespace StockManagementSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Spplier;

    public class Supplier
    {
        public Supplier()
        {
            this.ArticlesFromThisSupplier = new HashSet<Article>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(SpplierNameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Article> ArticlesFromThisSupplier { get; set; }
    }
}
