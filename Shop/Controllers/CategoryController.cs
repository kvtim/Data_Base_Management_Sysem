using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Shop.Models;
using MySql.Data.MySqlClient;
using Shop.DatabaseQueries.Selection.Categories;

namespace Shops.Controllers
{
    public class CategoryController : Controller
    {
        List<Category> categories = new List<Category>();

        CategoriesSelection categoriesSelection = new CategoriesSelection();

        public IActionResult Index()
        {
            categories = categoriesSelection.SelectAllCategories();

            return View(categories);
        }
    }
}