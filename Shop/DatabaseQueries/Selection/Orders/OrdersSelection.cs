using MySql.Data.MySqlClient;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.DatabaseQueries.Selection.Orders
{
    public class OrdersSelection
    {
        MySqlConnection connection;

        public OrdersSelection()
        {
            connection = new MySqlConnection("server=localhost;Database=mydb;user=root;password=admin");
        }

        public int? SelectLastOrderId()
        {
            connection.Open();

            int? orderId = null;

            using (MySqlCommand command = new MySqlCommand("select_last_order_id", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orderId = (int?)reader[0];
                    }
                }
            }

            connection.Close();

            return orderId;
        }

        public List<FullOrder> SelectCustomerFullOrders(int? customerId)
        {
            connection.Open();

            List<FullOrder> orders = new List<FullOrder>();

            using (MySqlCommand command = new MySqlCommand("select_customer_full_orders", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("c_id", MySqlDbType.Int32);
                parameter.Value = customerId;
                command.Parameters.Add(parameter);


                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (orders.Any() && orders.Last().OrderId == (int)reader[0])
                        {
                            orders.Last().Products.Add(
                                new ProductForOrder
                                {
                                    ProductCount = (int)reader[6],
                                    ProductName = (string)reader[7],
                                    ProductPrice = (int)reader[8]
                                });
                        }
                        else
                        {
                            orders.Add(
                                new FullOrder
                                {
                                    OrderId = (int)reader[0],
                                    OrderName = (string)reader[1],
                                    OrderStatus = (string)reader[2],
                                    OrderPrice = (int)reader[3],
                                    OrderTime = (DateTime)reader[4],
                                    Address = (string)reader[5],
                                    Products = new List<ProductForOrder>()
                                }
                                );

                            orders.Last().Products.Add(
                                new ProductForOrder
                                {
                                    ProductCount = (int)reader[6],
                                    ProductName = (string)reader[7],
                                    ProductPrice = (int)reader[8]
                                });
                        }
                    }
                }
            }

            connection.Close();

            return orders;
        }

        public List<FullOrder> SelectAllFullOrders()
        {
            connection.Open();

            List<FullOrder> orders = new List<FullOrder>();

            using (MySqlCommand command = new MySqlCommand("select_all_full_orders", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (orders.Any() && orders.Last().OrderId == (int)reader[0])
                        {
                            orders.Last().Products.Add(
                                new ProductForOrder
                                {
                                    ProductCount = (int)reader[6],
                                    ProductName = (string)reader[7],
                                    ProductPrice = (int)reader[8]
                                });
                        }
                        else
                        {
                            orders.Add(
                                new FullOrder
                                {
                                    OrderId = (int)reader[0],
                                    OrderName = (string)reader[1],
                                    OrderStatus = (string)reader[2],
                                    OrderPrice = (int)reader[3],
                                    OrderTime = (DateTime)reader[4],
                                    Address = (string)reader[5],
                                    Products = new List<ProductForOrder>()
                                }
                                );

                            orders.Last().Products.Add(
                                new ProductForOrder
                                {
                                    ProductCount = (int)reader[6],
                                    ProductName = (string)reader[7],
                                    ProductPrice = (int)reader[8]
                                });
                        }
                    }
                }
            }

            connection.Close();

            return orders;
        }
    }
}
