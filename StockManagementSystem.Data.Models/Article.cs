namespace StockManagementSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstants.Article;

    public class Article
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(ArticleNumberMaxLength)]
        public string ArticleNumber { get; set; } = null!;


        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;


        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }


        [Required]
        public decimal Price { get; set; }


        [Required]
        public int Qantity { get; set; }


        [ForeignKey(nameof(Supplier))]
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;


        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;



    }
}