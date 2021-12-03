using Microsoft.AspNetCore.Mvc;
using Shop.DatabaseQueries.Management.Addition;
using Shop.DatabaseQueries.Management.Deletion;
using Shop.DatabaseQueries.Management.Update;
using Shop.DatabaseQueries.Selection.Brands;
using Shop.DatabaseQueries.Selection.Categories;
using Shop.DatabaseQueries.Selection.Products;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class ProductManagementController : Controller
    {
        BrandsSelection brandsSelection = new BrandsSelection();

        ProductsSelection productsSelection = new ProductsSelection();

        CategoriesSelection categoriesSelection = new CategoriesSelection();

        AdditionQueries additionQueries = new AdditionQueries();

        DeletionQueries deletionQueries = new DeletionQueries();

        UpdateQueries updateQueries = new UpdateQueries();

        List<Product> products = new List<Product>();

        public IActionResult CategoryIndex(int? categoryId, int? brandId)
        {
            List<Brand> brands = brandsSelection.SelectBrandsOfTheSameCategory(categoryId);

            if (categoryId != 0 && categoryId != null && (brandId == null || brandId == 0))
            {
                products = productsSelection.SelectProductsOfOneCategory(categoryId);
            }
            else
            {
                products = productsSelection.SelectProductsOfOneCategoryAndBrand(categoryId, brandId);
            }

            if (categoryId != 0 && categoryId != null)
            {
                Category category = categoriesSelection.SelectCategoryById(categoryId);

                ViewData["CategoryId"] = category.CategoryId;
                ViewData["CategoryName"] = category.CategoryName;
            }

            ViewData["Brands"] = brands;

            ViewData["CurrentBrand"] = brandId ?? 0;

            return View(products);
        }

        public IActionResult BrandIndex(int? categoryId, int? brandId)
        {
            List<Category> categories = categoriesSelection.SelectCategoriesOfTheSameBrand(brandId);

            if (brandId != 0 && brandId != null && (categoryId == null || categoryId == 0))
            {
                products = productsSelection.SelectProductsOfOneBrand(brandId);
            }
            else
            {
                products = productsSelection.SelectProductsOfOneCategoryAndBrand(categoryId, brandId);
            }

            if (brandId != 0 && brandId != null)
            {
                Brand brand = brandsSelection.SelectBrandById(brandId);

                ViewData["BrandId"] = brand.BrandId;
                ViewData["BrandName"] = brand.BrandName;
            }

            ViewData["Categories"] = categories;

            ViewData["CurrentCategory"] = categoryId ?? 0;

            return View(products);
        }

        [HttpGet]
        public IActionResult Add(int? categoryId, string categoryName, int? brandId, string brandName)
        {
            ViewData["BrandsList"] = brandsSelection.SelectAllBrands();
            ViewData["CategoriesList"] = categoriesSelection.SelectAllCategories();

            if (categoryId != null)
            {
                ViewData["BrandName"] = null;
                ViewData["BrandId"] = null;
                ViewData["CategoryName"] = categoryName;
                ViewData["CategoryId"] = categoryId;
            }
            else
            {
                ViewData["CategoryName"] = null;
                ViewData["CategoryId"] = null;
                ViewData["BrandName"] = brandName;
                ViewData["BrandId"] = brandId;
            }

            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product, int? cId, int? bId)
        {
            if (cId != null)
            {
                product.CategoryId = (int)cId;

                if (!brandsSelection.SelectBrandToCategory(product.BrandId, product.CategoryId))
                    additionQueries.AddBrandToCategory(product.CategoryId, product.BrandId);

                additionQueries.AddProduct(product);

                return RedirectToAction("CategoryIndex", "ProductManagement", new { categoryId = cId });
            }
            else
            {
                product.BrandId = (int)bId;

                if (!brandsSelection.SelectBrandToCategory(product.BrandId, product.CategoryId))
                    additionQueries.AddBrandToCategory(product.CategoryId, product.BrandId);

                additionQueries.AddProduct(product);

                return RedirectToAction("BrandIndex", "ProductManagement", new { brandId = bId });
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id, int? categoryId, int? brandId)
        {
            ViewData["BrandsList"] = brandsSelection.SelectAllBrands();
            ViewData["CategoriesList"] = categoriesSelection.SelectAllCategories();

            if (id != null)
            {
                Product product = productsSelection.SelectProductById(id);

                if (categoryId != null)
                {
                    ViewData["BrandId"] = null;
                    ViewData["CategoryId"] = categoryId;
                }
                else
                {
                    ViewData["CategoryId"] = null;
                    ViewData["BrandId"] = brandId;
                }

                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Product product, int? cId, int? bId, int? id)
        {
            Product prevProduct = productsSelection.SelectProductById(id);

            if (productsSelection.SelectProductsOfOneCategoryAndBrand(prevProduct.CategoryId, prevProduct.BrandId).Count() <= 1)
                deletionQueries.DeleteBrandToCategory(prevProduct.BrandId, prevProduct.CategoryId);

            if (!brandsSelection.SelectBrandToCategory(product.BrandId, product.CategoryId))
                additionQueries.AddBrandToCategory(product.CategoryId, product.BrandId);

            updateQueries.UpdateProduct(product, id);


            if (cId != null)
            {
                return RedirectToAction("CategoryIndex", "ProductManagement", new { categoryId = cId });
            }
            else
            {
                return RedirectToAction("BrandIndex", "ProductManagement", new { brandId = bId });
            }
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? id, int? categoryId, int? brandId)
        {
            if (id != null)
            {
                FullProduct product = productsSelection.SelectFullProduct(id);

                if (product != null)
                {
                    if (categoryId != null)
                    {
                        ViewData["CategoryId"] = categoryId;
                        ViewData["BrandId"] = null;
                    }
                    else
                    {
                        ViewData["BrandId"] = brandId;
                        ViewData["CategoryId"] = null;
                    }

                    return View(product);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int? id, int? categoryId, int? brandId)
        {
            if (id != null)
            {
                Product product = productsSelection.SelectProductById(id);

                deletionQueries.DeleteProduct(id);

                if (!productsSelection.SelectProductsOfOneCategoryAndBrand(product.CategoryId, product.BrandId).Any())
                    deletionQueries.DeleteBrandToCategory(product.BrandId, product.CategoryId);

                if (categoryId != null)
                {
                    return RedirectToAction("CategoryIndex", "ProductManagement", new { categoryId = categoryId });
                }
                else
                {
                    return RedirectToAction("BrandIndex", "ProductManagement", new { brandId = brandId });
                }
            }
            return NotFound();
        }
    }
}
