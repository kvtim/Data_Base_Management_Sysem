using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.DatabaseQueries.Management.Deletion
{
    public class DeletionQueries
    {
        MySqlConnection connection;

        public DeletionQueries()
        {
            connection = new MySqlConnection("server=localhost;Database=mydb;user=root;password=admin");
        }

        public void DeleteCategory(int? id)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("delete_category", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("c_id", MySqlDbType.Int32);
                parameter.Value = id;
                command.Parameters.Add(parameter);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch { }
            }

            connection.Close();
        }

        public void DeleteCustomer(int? customerId)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("delete_customer", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("c_id", MySqlDbType.Int32);
                parameter.Value = customerId;
                command.Parameters.Add(parameter);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch { }
            }

            connection.Close();
        }

        public void DeleteBrand(int? id)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("delete_brand", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("b_id", MySqlDbType.Int32);
                parameter.Value = id;
                command.Parameters.Add(parameter);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch { }
            }

            connection.Close();
        }

        public void DeleteProduct(int? id)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("delete_product", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("p_id", MySqlDbType.Int32);
                parameter.Value = id;
                command.Parameters.Add(parameter);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch { }
            }

            connection.Close();
        }

        public void DeleteOrder(int orderId)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("delete_order", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("o_id", MySqlDbType.Int32);
                parameter.Value = orderId;
                command.Parameters.Add(parameter);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch { }
            }

            connection.Close();
        }

        public void DeleteBrandToCategory(int brandId, int categoryId)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("delete_brand_to_category", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter[] parameters = new MySqlParameter[2];

                parameters[0] = new MySqlParameter("b_id", MySqlDbType.Int32);
                parameters[0].Value = brandId;

                parameters[1] = new MySqlParameter("c_id", MySqlDbType.Int32);
                parameters[1].Value = categoryId;

                command.Parameters.AddRange(parameters);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch { }
            }

            connection.Close();
        }
    }
}
