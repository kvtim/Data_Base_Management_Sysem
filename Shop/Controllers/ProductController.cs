using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Shop.Models;
using MySql.Data.MySqlClient;
using System.Data;
using Shop.DatabaseQueries.Selection.Brands;
using Shop.DatabaseQueries.Selection.Products;
using Shop.DatabaseQueries.Selection.Categories;

namespace Shops.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        BrandsSelection brandsSelection = new BrandsSelection();

        ProductsSelection productsSelection = new ProductsSelection();

        CategoriesSelection categoriesSelection = new CategoriesSelection();

        List<Product> products = new List<Product>();

        public IActionResult Index(int? categoryId, int? brand)
        {
            List<Brand> brands = brandsSelection.SelectBrandsOfTheSameCategory(categoryId);

            if (categoryId != 0 && categoryId != null && (brand == null || brand == 0))
            {
                products = productsSelection.SelectProductsOfOneCategory(categoryId);
            }
            else
            {
                products = productsSelection.SelectProductsOfOneCategoryAndBrand(categoryId, brand);
            }

            if (categoryId != 0 && categoryId != null)
            {
                Category category = categoriesSelection.SelectCategoryById(categoryId);

                ViewData["categoryId"] = category.CategoryId;
                ViewData["CategoryName"] = category.CategoryName;
            }

            ViewData["Brands"] = brands;

            ViewData["CurrentBrand"] = brand ?? 0;

            return View(products);
        }
    }
}
