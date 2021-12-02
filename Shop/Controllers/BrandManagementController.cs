using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Shop.DatabaseQueries.Management.Addition;
using Shop.DatabaseQueries.Management.Deletion;
using Shop.DatabaseQueries.Management.Update;
using Shop.DatabaseQueries.Selection.Brands;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class BrandManagementController : Controller
    {
        AdditionQueries additionQueries = new AdditionQueries();

        BrandsSelection brandsSelection = new BrandsSelection();

        UpdateQueries updateQueries = new UpdateQueries();

        DeletionQueries deletionQueries = new DeletionQueries();

        public IActionResult Index()
        {
            return View(brandsSelection.SelectAllBrands());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Brand brand)
        {
            additionQueries.AddBrand(brand.BrandName);

            return RedirectToAction("Index", "BrandManagement");
        }

        [HttpGet]
        public IActionResult Edit(int? brandId)
        {
            if (brandId != null)
            {
                return View(brandsSelection.SelectBrandById(brandId));
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Brand brand, int? brandId)
        {
            updateQueries.UpdateBrand(brandId, brand.BrandName);

            return RedirectToAction("Index", "BrandManagement");
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? brandId)
        {
            if (brandId != null)
            {
                Brand brand = brandsSelection.SelectBrandById(brandId);

                if (brand != null)
                    return View(brand);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int? brandId)
        {
            if (brandId != null)
            {
                deletionQueries.DeleteBrand(brandId);

                return RedirectToAction("Index", "BrandManagement");
            }
            return NotFound();
        }
    }
}
