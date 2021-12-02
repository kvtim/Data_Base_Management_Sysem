using MySql.Data.MySqlClient;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.DatabaseQueries.Management.Addition
{
    public class AdditionQueries
    {
        MySqlConnection connection;

        public AdditionQueries()
        {
            connection = new MySqlConnection("server=localhost;Database=mydb;user=root;password=admin");
        }

        public void AddCustomer(RegisterModel model)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("add_customer", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter[] parameters = new MySqlParameter[4];

                parameters[0] = new MySqlParameter("c_name", MySqlDbType.VarChar);
                parameters[0].Value = model.Name;

                parameters[1] = new MySqlParameter("c_surname", MySqlDbType.VarChar);
                parameters[1].Value = model.Surname;

                parameters[2] = new MySqlParameter("c_email", MySqlDbType.VarChar);
                parameters[2].Value = model.Email;

                parameters[3] = new MySqlParameter("c_password", MySqlDbType.VarChar);
                parameters[3].Value = model.Password;

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void AddProduct(Product product)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("add_product", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter[] parameters = new MySqlParameter[5];

                parameters[0] = new MySqlParameter("p_name", MySqlDbType.VarChar);
                parameters[0].Value = product.ProductName;

                parameters[1] = new MySqlParameter("p_description", MySqlDbType.VarChar);
                parameters[1].Value = product.ProductDescription;

                parameters[2] = new MySqlParameter("p_price", MySqlDbType.Int32);
                parameters[2].Value = product.ProductPrice;

                parameters[3] = new MySqlParameter("c_id", MySqlDbType.Int32);
                parameters[3].Value = product.CategoryId;

                parameters[4] = new MySqlParameter("b_id", MySqlDbType.Int32);
                parameters[4].Value = product.BrandId;

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void AddCategory(string categoryName)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("add_category", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("c_name", MySqlDbType.VarChar);

                parameter.Value = categoryName;

                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void AddBrand(string brandName)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("add_brand", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("b_name", MySqlDbType.VarChar);

                parameter.Value = brandName;

                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void AddBrandToCategory(int? categoryId, int? brandId)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("add_brand_to_category", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter[] parameters = new MySqlParameter[2];

                parameters[0] = new MySqlParameter("b_id", MySqlDbType.Int32);
                parameters[0].Value = brandId;

                parameters[1] = new MySqlParameter("c_id", MySqlDbType.Int32);
                parameters[1].Value = categoryId;

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void AddOrder(string email, int? price, int? customerId)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("add_order", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter[] parameters = new MySqlParameter[3];

                parameters[0] = new MySqlParameter("o_name", MySqlDbType.VarChar);
                parameters[0].Value = $"Order for {email}";

                parameters[1] = new MySqlParameter("o_price", MySqlDbType.Int32);
                parameters[1].Value = price;

                parameters[2] = new MySqlParameter("customer_id", MySqlDbType.Int32);
                parameters[2].Value = customerId;

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public void AddDelivery(string customerEmail, string address, int? orderId)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("add_delivery", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter[] parameters = new MySqlParameter[3];

                parameters[0] = new MySqlParameter("d_name", MySqlDbType.VarChar);
                parameters[0].Value = $"Delivery for {customerEmail}";

                parameters[1] = new MySqlParameter("d_address", MySqlDbType.VarChar);
                parameters[1].Value = address;

                parameters[2] = new MySqlParameter("order_id", MySqlDbType.Int32);
                parameters[2].Value = orderId;

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void AddOrderToProduct(int? orderId, int? productId, int? productCount)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("add_order_to_product", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter[] parameters = new MySqlParameter[3];

                parameters[0] = new MySqlParameter("order_id", MySqlDbType.Int32);
                parameters[0].Value = orderId;

                parameters[1] = new MySqlParameter("product_id", MySqlDbType.Int32);
                parameters[1].Value = productId;

                parameters[2] = new MySqlParameter("product_count", MySqlDbType.Int32);
                parameters[2].Value = productCount;

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}
