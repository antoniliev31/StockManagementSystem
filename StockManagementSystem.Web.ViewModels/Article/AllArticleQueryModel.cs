namespace StockManagementSystem.Web.ViewModels.Article
{
    using System.ComponentModel.DataAnnotations;
    using Enums;
    using static Common.GeneralApplicationConstants;

    public class AllArticleQueryModel
    {
        public AllArticleQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.ArticlesPerPage = EntitiesPerPage;
            this.Categories = new HashSet<string>();
            this.Suppliers = new HashSet<string>();
            this.Articles = new HashSet<ArticleAllViewModel>();
        }

        [Display(Name = "CATEGORY")]
        public string? Category { get; set; }

        [Display(Name = "SUPPLIER")]
        public string? Supplier { get; set; }

        [Display(Name = "SEARCH")]
        public string? SearchString { get; set; }

        [Display(Name = "SORT")]
        public ArticleSorting ArticleSorting { get; set; }

        [Display(Name = "Cyrrent page")]
        public int CurrentPage { get; set; }

        [Display(Name = "Articles per page:")]
        public int ArticlesPerPage { get; set; }

        [Display(Name = "Total articles")]
        public int TotalArticles { get; set; }

        public string ArticleNumber { get; set; } = null!;

        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<string> Suppliers { get; set; }
        public IEnumerable<ArticleAllViewModel> Articles { get; set; }
    }
}
