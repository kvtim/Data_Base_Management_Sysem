using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Shop.DatabaseQueries.Management.Addition;
using Shop.DatabaseQueries.Management.Deletion;
using Shop.DatabaseQueries.Management.Update;
using Shop.DatabaseQueries.Selection.Categories;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class CategoryManagementController : Controller
    {
        AdditionQueries additionQueries = new AdditionQueries();

        CategoriesSelection categoriesSelection = new CategoriesSelection();

        UpdateQueries updateQueries = new UpdateQueries();

        DeletionQueries deletionQueries = new DeletionQueries();

        public IActionResult Index()
        {
            return View(categoriesSelection.SelectAllCategories());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            additionQueries.AddCategory(category.CategoryName);

            return RedirectToAction("Index", "CategoryManagement");
        }

        [HttpGet]
        public IActionResult Edit(int? categoryId)
        {
            if (categoryId != null)
            {
                return View(categoriesSelection.SelectCategoryById(categoryId));
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Category category, int? categoryId)
        {
            updateQueries.UpdateCategory(categoryId, category.CategoryName);

            return RedirectToAction("Index", "CategoryManagement");
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? categoryId)
        {
            if (categoryId != null)
            {
                Category category = categoriesSelection.SelectCategoryById(categoryId);

                if (category != null)
                    return View(category);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int? categoryId)
        {
            if (categoryId != null)
            {
                deletionQueries.DeleteCategory(categoryId);

                return RedirectToAction("Index", "CategoryManagement");
            }
            return NotFound();
        }
    }
}
