using MySql.Data.MySqlClient;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.DatabaseQueries.Selection.Products
{
    public class ProductsSelection
    {
        MySqlConnection connection;

        public ProductsSelection()
        {
            connection = new MySqlConnection("server=localhost;Database=mydb;user=root;password=admin");
        }

        public Product SelectProductById(int? id)
        {
            connection.Open();

            Product product = null;

            using (MySqlCommand command = new MySqlCommand("select_product_by_id", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("p_id", MySqlDbType.Int32);
                parameter.Value = id;
                command.Parameters.Add(parameter);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product = new Product
                        {
                            ProductId = (int)reader[0],
                            ProductName = (string)reader[1],
                            ProductDescription = (string)reader[2],
                            ProductPrice = (int)reader[3],
                            CategoryId = (int)reader[4],
                            BrandId = (int)reader[5]
                        };
                    }
                }
            }

            connection.Close();

            return product;
        }

        public FullProduct SelectFullProduct(int? id)
        {
            connection.Open();

            FullProduct product = null;

            using (MySqlCommand command = new MySqlCommand("select_full_product", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("p_id", MySqlDbType.Int32);
                parameter.Value = id;
                command.Parameters.Add(parameter);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product = new FullProduct
                        {
                            ProductId = (int)reader[0],
                            ProductName = (string)reader[1],
                            ProductDescription = (string)reader[2],
                            ProductPrice = (int)reader[3],
                            CategoryName = (string)reader[4],
                            BrandName = (string)reader[5]
                        };
                    }
                }
            }

            connection.Close();

            return product;
        }

        public List<Product> SelectProductsOfOneCategory(int? categoryId)
        {
            connection.Open();

            List<Product> products = new List<Product>();

            using (MySqlCommand command = new MySqlCommand("select_products_of_one_category", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("c_id", MySqlDbType.Int32);
                parameter.Value = categoryId;
                command.Parameters.Add(parameter);


                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(
                            new Product
                            {
                                ProductId = (int)reader[0],
                                ProductName = (string)reader[1],
                                ProductDescription = (string)reader[2],
                                ProductPrice = (int)reader[3],
                                CategoryId = (int)reader[4],
                                BrandId = (int)reader[5]
                            }
                            );
                    }
                }
            }

            connection.Close();

            return products;
        }

        public List<Product> SelectProductsOfOneBrand(int? brandId)
        {
            connection.Open();

            List<Product> products = new List<Product>();

            using (MySqlCommand command = new MySqlCommand("select_products_of_one_brand", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("b_id", MySqlDbType.Int32);
                parameter.Value = brandId;
                command.Parameters.Add(parameter);


                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(
                            new Product
                            {
                                ProductId = (int)reader[0],
                                ProductName = (string)reader[1],
                                ProductDescription = (string)reader[2],
                                ProductPrice = (int)reader[3],
                                CategoryId = (int)reader[4],
                                BrandId = (int)reader[5]
                            }
                            );
                    }
                }
            }

            connection.Close();

            return products;
        }

        public List<Product> SelectProductsOfOneCategoryAndBrand(int? categoryId, int? brandId)
        {
            connection.Open();

            List<Product> products = new List<Product>();

            using (MySqlCommand command = new MySqlCommand("select_products_of_one_category_and_brand", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter[] parameters = new MySqlParameter[2];

                parameters[0] = new MySqlParameter("c_id", MySqlDbType.Int32);
                parameters[0].Value = categoryId;

                parameters[1] = new MySqlParameter("b_id", MySqlDbType.Int32);
                parameters[1].Value = brandId;

                command.Parameters.AddRange(parameters);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(
                            new Product
                            {
                                ProductId = (int)reader[0],
                                ProductName = (string)reader[1],
                                ProductDescription = (string)reader[2],
                                ProductPrice = (int)reader[3],
                                CategoryId = (int)reader[4],
                                BrandId = (int)reader[5]
                            }
                            );
                    }
                }
            }

            connection.Close();

            return products;
        }

        public List<Product> SelectProductsFound(string textSearch)
        {
            connection.Open();

            List<Product> products = new List<Product>();

            using (MySqlCommand command = new MySqlCommand("select_products_found", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("search_text", MySqlDbType.VarChar);
                parameter.Value = textSearch;
                command.Parameters.Add(parameter);


                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(
                            new Product
                            {
                                ProductId = (int)reader[0],
                                ProductName = (string)reader[1],
                                ProductDescription = (string)reader[2],
                                ProductPrice = (int)reader[3],
                                CategoryId = (int)reader[4],
                                BrandId = (int)reader[5]
                            }
                            );
                    }
                }
            }

            connection.Close();

            return products;
        }
    }
}
