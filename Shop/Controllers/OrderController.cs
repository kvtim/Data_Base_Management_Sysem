using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Shop.DatabaseQueries.Management.Deletion;
using Shop.DatabaseQueries.Selection.Orders;
using Shop.DatabaseQueries.Selection.Users;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class OrderController : Controller
    {
        CustomersSelection customersSelection = new CustomersSelection();

        OrdersSelection ordersSelection = new OrdersSelection();

        DeletionQueries deletionQueries = new DeletionQueries();

        public ActionResult Index()
        {
            int? customerId = customersSelection.SelectCustomerIdByEmail(User.Identity.Name);

            List<FullOrder> orders = ordersSelection.SelectCustomerFullOrders(customerId);

            return View(orders);
        }

        public ActionResult Delete(int orderId)
        {
            deletionQueries.DeleteOrder(orderId);

            return RedirectToAction("Index");
        }
    }
}
