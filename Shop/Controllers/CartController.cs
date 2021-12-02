using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Shop.DatabaseQueries.Management.Addition;
using Shop.DatabaseQueries.Selection.Orders;
using Shop.DatabaseQueries.Selection.Products;
using Shop.DatabaseQueries.Selection.Users;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        private Cart _cart;

        ProductsSelection productsSelection = new ProductsSelection();

        CustomersSelection customersSelection = new CustomersSelection();

        AdditionQueries additionQueries = new AdditionQueries();

        OrdersSelection ordersSelection = new OrdersSelection();

        public CartController(Cart cart)
        {
            _cart = cart;
        }

        public IActionResult Index()
        {
            ViewData["Price"] = _cart.Price;
            return View(_cart.Items.Values);
        }

        [Authorize]
        public IActionResult Add(int id, string returnUrl)
        {
            Product product = productsSelection.SelectProductById(id);

            if (product != null)
            {
                _cart.AddToCart(product);
            }
            return Redirect(returnUrl);
        }

        [Authorize]
        public IActionResult Checkout(string address)
        {
            int? customerId = customersSelection.SelectCustomerIdByEmail(User.Identity.Name);

            if (customerId != null)
            {
                additionQueries.AddOrder(User.Identity.Name, _cart.Price, customerId);

                int? orderId = ordersSelection.SelectLastOrderId();

                if (orderId != null)
                {
                    additionQueries.AddDelivery(User.Identity.Name, address, orderId);

                    foreach (CartItem item in _cart.Items.Values)
                    {
                        additionQueries.AddOrderToProduct(orderId, item.Product.ProductId, item.Quantity);
                    }
                }
            }

            _cart.ClearAll();
            return RedirectToAction("Index", "Order");
        }

        public IActionResult Delete(int id)
        {
            _cart.RemoveFromCart(id);
            return RedirectToAction("Index");
        }
    }
}
