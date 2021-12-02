using Microsoft.AspNetCore.Mvc;
using Shop.DatabaseQueries.Management.Deletion;
using Shop.DatabaseQueries.Management.Update;
using Shop.DatabaseQueries.Selection.Users;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class CustomerManagementController : Controller
    {
        CustomersSelection customersSelection = new CustomersSelection();

        UpdateQueries updateQueries = new UpdateQueries();

        DeletionQueries deletionQueries = new DeletionQueries();

        public IActionResult Index()
        {
            return View(customersSelection.SelectAllCustomers());
        }

        public IActionResult MakeAdmin(int? customerId)
        {
            updateQueries.MakeAdmin(customerId);

            return RedirectToAction("Index", "CustomerManagement");
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? customerId)
        {
            if (customerId != null)
            {
                Customer customer = customersSelection.SelectCustomerById(customerId);

                if (customer != null)
                    return View(customer);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int? customerId)
        {
            if (customerId != null)
            {
                deletionQueries.DeleteCustomer(customerId);

                return RedirectToAction("Index", "CustomerManagement");
            }
            return NotFound();
        }
    }
}
