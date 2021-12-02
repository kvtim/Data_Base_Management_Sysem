using MySql.Data.MySqlClient;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.DatabaseQueries.Selection.Users
{
    public class CustomersSelection
    {
        MySqlConnection connection;

        public CustomersSelection()
        {
            connection = new MySqlConnection("server=localhost;Database=mydb;user=root;password=admin");
        }

        public List<Customer> SelectAllCustomers()
        {
            connection.Open();

            List<Customer> customers = new List<Customer>();

            using (MySqlCommand command = new MySqlCommand("select_all_customers", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(
                            new Customer
                            {
                                CustomerId = (int)reader[0],
                                CustomerName = (string)reader[1],
                                CustomerSurname = (string)reader[2],
                                CustomerEmail = (string)reader[3],
                                CustomerPassword = (string)reader[4],
                                RegistrationTime = (DateTime)reader[5],
                                RoleId = (int)reader[6]
                            }
                            );
                    }
                }
            }

            connection.Close();

            return customers;
        }


        public int? SelectCustomerIdByEmail(string email)
        {
            connection.Open();

            int? id = null;
            using (MySqlCommand command = new MySqlCommand("select_customer_id_by_email", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("c_email", MySqlDbType.VarChar);
                parameter.Value = email;
                command.Parameters.Add(parameter);


                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        id = (int?)reader[0];
                    }
                }
            }

            connection.Close();

            return id;
        }

        public Customer SelectCustomerById(int? customerId)
        {
            connection.Open();

            Customer customer = null;
            using (MySqlCommand command = new MySqlCommand("select_customer", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("id", MySqlDbType.Int32);
                parameter.Value = customerId;
                command.Parameters.Add(parameter);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customer = new Customer
                        {
                            CustomerId = (int)reader[0],
                            CustomerName = (string)reader[1],
                            CustomerSurname = (string)reader[2],
                            CustomerEmail = (string)reader[3],
                            CustomerPassword = (string)reader[4],
                            RegistrationTime = (DateTime)reader[5],
                            RoleId = (int)reader[6]
                        };
                    }
                }
            }

            connection.Close();

            return customer;
        }

        public Customer SelectCustomerByEmail(string email)
        {
            connection.Open();

            Customer customer = null;
            using (MySqlCommand command = new MySqlCommand("select_customer_by_email", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("c_email", MySqlDbType.VarChar);
                parameter.Value = email;
                command.Parameters.Add(parameter);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customer = new Customer
                        {
                            CustomerId = (int)reader[0],
                            CustomerName = (string)reader[1],
                            CustomerSurname = (string)reader[2],
                            CustomerEmail = (string)reader[3],
                            CustomerPassword = (string)reader[4],
                            RegistrationTime = (DateTime)reader[5],
                            RoleId = (int)reader[6]
                        };
                    }
                }
            }

            connection.Close();

            return customer;
        }

        public Customer SelectCustomerByEmailAndPassword(string email, string password)
        {
            connection.Open();

            Customer customer = null;
            using (MySqlCommand command = new MySqlCommand("select_customer_by_email_and_password", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter[] parameters = new MySqlParameter[2];

                parameters[0] = new MySqlParameter("c_email", MySqlDbType.VarChar);
                parameters[0].Value = email;

                parameters[1] = new MySqlParameter("c_password", MySqlDbType.VarChar);
                parameters[1].Value = password;

                command.Parameters.AddRange(parameters);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customer = new Customer
                        {
                            CustomerId = (int)reader[0],
                            CustomerName = (string)reader[1],
                            CustomerSurname = (string)reader[2],
                            CustomerEmail = (string)reader[3],
                            CustomerPassword = (string)reader[4],
                            RegistrationTime = (DateTime)reader[5],
                            RoleId = (int)reader[6]
                        };
                    }
                }
            }

            connection.Close();

            return customer;
        }

        public string SelectRoleName(int? roleId)
        {
            connection.Open();

            string role = null;

            using (MySqlCommand command = new MySqlCommand("select_role_name", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("r_id", MySqlDbType.Int32);
                parameter.Value = roleId;
                command.Parameters.Add(parameter);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        role = (string)reader[0];
                    }
                }
            }

            connection.Close();

            return role;
        }
    }
}
