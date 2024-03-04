# StockManagementSystem
Stock Management System

Project Structure

project-root/
│
├── Controllers/
│   ├── ArticleController.cs
│   └── CategoryController.cs
│   └── HomeController.cs
│   └── SupplierController.cs
│
├── Data/
│   ├── SMSDbContext.cs
│   └── Models/
│       └── Article.cs
│       └── Category.cs
│       └── Supplier.cs
│
├── Services/
│   └── Data/
│       ├── ArticleService.cs
│       ├── CategoryService.cs
│       ├── SupplierService.cs
│       ├── Interfaces/
│       │   ├── IArticleService.cs
│       │   ├── ICategoryService.cs
│       │   └── ISupplierService.cs
│       └── Models/
│           └── Article/
│               ├── ArticleFormModel.cs
│               ├── ArticleForDeleteViewModel.cs
│               ├── ArticleAllViewModel.cs
│               └── ArticleDetailsViewModel.cs
│
├── ViewModels/
│   ├── Article/
│   │   ├── AllArticleQueryModel.cs
│   │   ├── ArticleAllViewModel.cs
│   │   ├── ArticleDetailsViewModel.cs
│   │   ├── ArticleForDeleteViewModel.cs
│   │   ├── ArticleFormModel.cs
│   │   ├── ArticleSelectCategoryFormModel.cs
│   │   ├── ArticleSelectSupplierFormModel.cs
│   │
│   ├── Category/
│   │   └── CategoryFormModel.cs
│   └── Category/
│       └── SupplierFormModel.cs
│
├── Views/
│   ├── Article/
│   │   ├── Add.cshtml
│   │   ├── All.cshtml
│   │   ├── Delete.cshtml
│   │   ├── Details.cshtml
│   │   └── Edit.cshtml
│   ├── Category/
│   │   └── Add.cshtml
│   ├── Home/
│   │   └── Index.cshtml
│   ├── Shared/
│   │   ├── _ArticleFormPartial.cshtml
│   │   ├── _ArticlePartial.cshtml
│   │   ├── _Layout.cshtml
│   │   ├── _LoginPartial.cshtml
│   │   └── _ValidationScriptsPartial.cshtml
│   ├── Supplier/
│       └── Add.cshtml
│
├── wwwroot/
│   ├── css/
│   │   └── styles.css
│   └── js/
│       └── script.js
│
├── appsettings.json
├── Program.cs
├── Startup.cs
├── README.md
└── LICENSE
