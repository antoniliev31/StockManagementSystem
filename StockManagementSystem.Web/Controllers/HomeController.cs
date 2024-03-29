﻿namespace StockManagementSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StockManagementSystem.Web.Models;
    using System.Diagnostics;

    [Authorize]
    public class HomeController : Controller
    {
        
        public HomeController()
        {
            
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
