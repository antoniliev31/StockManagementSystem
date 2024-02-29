namespace StockManagementSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Article
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Qantity { get; set; }

    }
}
