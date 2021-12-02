using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Shop.DatabaseQueries.Management.Addition;
using Shop.DatabaseQueries.Selection.Users;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class AccountController : Controller
    {
        CustomersSelection customersSelection = new CustomersSelection();
        AdditionQueries additionQueries = new AdditionQueries();

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                int? id = customersSelection.SelectCustomerIdByEmail(model.Email);

                if (id == null)
                {
                    additionQueries.AddCustomer(model);

                    Customer customer = customersSelection.SelectCustomerByEmail(model.Email);

                    if (customer != null)
                    {
                        await Authenticate(customer);

                        return RedirectToAction("Index", "Category");
                    }
                }

                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = customersSelection.SelectCustomerByEmailAndPassword(model.Email, model.Password);
   
                if (customer != null)
                {
                    await Authenticate(customer);

                    return RedirectToAction("Index", "Category");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(Customer user)
        {
            string role = customersSelection.SelectRoleName(user.RoleId);

            if (role != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.CustomerEmail),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                };

                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
