using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Shop.DatabaseQueries.Management.Deletion;
using Shop.DatabaseQueries.Management.Update;
using Shop.DatabaseQueries.Selection.Orders;
using Shop.DatabaseQueries.Selection.Users;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class OrderManagementController : Controller
    {
        OrdersSelection ordersSelection = new OrdersSelection();

        CustomersSelection customerSelection = new CustomersSelection();

        UpdateQueries updateQueries = new UpdateQueries();

        DeletionQueries deletionQueries = new DeletionQueries();

        public IActionResult Index(int? customerId)
        {
            ViewData["Customers"] = customerSelection.SelectAllCustomers();

            ViewData["CurrentCustomer"] = customerId ?? 0;

            if (customerId == null || customerId == 0)
                return View(ordersSelection.SelectAllFullOrders());

            return View(ordersSelection.SelectCustomerFullOrders(customerId));
        }

        public IActionResult ChangeStatus(int? orderId, string newStatus, int? selectedCustomer)
        {
            updateQueries.UpdateOrderStatus(orderId, newStatus);

            return RedirectToAction("Index", new { customerId = selectedCustomer });
        }

        public ActionResult Delete(int orderId, int? selectedCustomer)
        {
            deletionQueries.DeleteOrder(orderId);

            return RedirectToAction("Index", new { customerId = selectedCustomer });
        }
    }
}
