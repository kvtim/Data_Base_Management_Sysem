using MySql.Data.MySqlClient;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.DatabaseQueries.Management.Update
{
    public class UpdateQueries
    {
        MySqlConnection connection;

        public UpdateQueries()
        {
            connection = new MySqlConnection("server=localhost;Database=mydb;user=root;password=admin");
        }

        public void UpdateCategory(int? categoryId, string categoryName)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("update_category", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter[] parameters = new MySqlParameter[2];

                parameters[0] = new MySqlParameter("c_id", MySqlDbType.Int32);
                parameters[0].Value = categoryId;

                parameters[1] = new MySqlParameter("c_name", MySqlDbType.VarChar);
                parameters[1].Value = categoryName;

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void UpdateBrand(int? brandId, string brandName)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("update_brand", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter[] parameters = new MySqlParameter[2];

                parameters[0] = new MySqlParameter("b_id", MySqlDbType.Int32);
                parameters[0].Value = brandId;

                parameters[1] = new MySqlParameter("b_name", MySqlDbType.VarChar);
                parameters[1].Value = brandName;

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void UpdateProduct(Product product, int? id)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("update_product", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter[] parameters = new MySqlParameter[6];

                parameters[0] = new MySqlParameter("p_id", MySqlDbType.Int32);
                parameters[0].Value = id;

                parameters[1] = new MySqlParameter("p_name", MySqlDbType.VarChar);
                parameters[1].Value = product.ProductName;

                parameters[2] = new MySqlParameter("p_description", MySqlDbType.VarChar);
                parameters[2].Value = product.ProductDescription;

                parameters[3] = new MySqlParameter("p_price", MySqlDbType.Int32);
                parameters[3].Value = product.ProductPrice;

                parameters[4] = new MySqlParameter("c_id", MySqlDbType.Int32);
                parameters[4].Value = product.CategoryId;

                parameters[5] = new MySqlParameter("b_id", MySqlDbType.Int32);
                parameters[5].Value = product.BrandId;

                command.Parameters.AddRange(parameters);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch { }
            }

            connection.Close();
        }


        public void UpdateOrderStatus(int? orderId, string newStatus)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("update_order_status", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter[] parameters = new MySqlParameter[2];

                parameters[0] = new MySqlParameter("o_status", MySqlDbType.VarChar);
                parameters[0].Value = newStatus;

                parameters[1] = new MySqlParameter("o_id", MySqlDbType.Int32);
                parameters[1].Value = orderId;

                command.Parameters.AddRange(parameters);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void MakeAdmin(int? customerId)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("make_admin", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("c_id", MySqlDbType.Int32);
                parameter.Value = customerId;

                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}
