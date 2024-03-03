namespace StockManagementSystem.Common
{
    public class EntityValidationConstants
    {
        public static class Article
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 20;

            public const int ArticleNumberMinLength = 8;
            public const int ArticleNumberMaxLength = 8;

            public const int DescriptionMaxLength = 150;

            public const string PriceMinValue = "0";
            public const string PriceMaxValue = "2000";

            public const int QantityMinValue = 0;
        }

        public static class Category
        {
            public const int CategoryNameMinLength = 2;
            public const int CategoryNameMaxLength = 20;
        }

        public static class Spplier
        {
            public const int SpplierNameMinLength = 2;
            public const int SpplierNameMaxLength = 20;
        }


    }
}
