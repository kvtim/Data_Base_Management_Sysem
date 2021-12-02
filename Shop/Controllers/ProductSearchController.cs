using Microsoft.AspNetCore.Mvc;
using Shop.DatabaseQueries.Management.Addition;
using Shop.DatabaseQueries.Management.Deletion;
using Shop.DatabaseQueries.Management.Update;
using Shop.DatabaseQueries.Selection.Brands;
using Shop.DatabaseQueries.Selection.Products;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class ProductSearchController : Controller
    {
        ProductsSelection productsSelection = new ProductsSelection();

        UpdateQueries updateQueries = new UpdateQueries();

        DeletionQueries deletionQueries = new DeletionQueries();

        BrandsSelection brandsSelection = new BrandsSelection();

        AdditionQueries additionQueries = new AdditionQueries();

        public IActionResult Index(string textSearch)
        {
            if (textSearch != null)
            {
                return View(productsSelection.SelectProductsFound(textSearch));
            }
            else
                return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                Product product = productsSelection.SelectProductById(id);

                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Product product, int? id)
        {
            Product prevProduct = productsSelection.SelectProductById(id);

            if (!productsSelection.SelectProductsOfOneCategoryAndBrand(prevProduct.CategoryId, prevProduct.BrandId).Any())
                deletionQueries.DeleteBrandToCategory(prevProduct.BrandId, prevProduct.CategoryId);

            if (!brandsSelection.SelectBrandToCategory(product.BrandId, product.CategoryId))
                additionQueries.AddBrandToCategory(product.CategoryId, product.BrandId);

            updateQueries.UpdateProduct(product, id);

            return RedirectToAction("Index", "Category");
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Product product = productsSelection.SelectProductById(id);

                if (product != null)
                {
                    return View(product);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                Product product = productsSelection.SelectProductById(id);

                deletionQueries.DeleteProduct(id);

                if (!productsSelection.SelectProductsOfOneCategoryAndBrand(product.CategoryId, product.BrandId).Any())
                    deletionQueries.DeleteBrandToCategory(product.BrandId, product.CategoryId);

                return RedirectToAction("Index", "Category");

            }
            return NotFound();
        }
    }
}
