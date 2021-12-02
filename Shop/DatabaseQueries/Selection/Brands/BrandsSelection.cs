using MySql.Data.MySqlClient;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.DatabaseQueries.Selection.Brands
{
    public class BrandsSelection
    {
        MySqlConnection connection;

        public BrandsSelection()
        {
            connection = new MySqlConnection("server=localhost;Database=mydb;user=root;password=admin");
        }

        public List<Brand> SelectBrandsOfTheSameCategory(int? categoryId)
        {
            connection.Open();

            List<Brand> brands = new List<Brand>();

            using (MySqlCommand command = new MySqlCommand("select_brands_of_the_same_category", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("c_id", MySqlDbType.Int32);
                parameter.Value = categoryId;
                command.Parameters.Add(parameter);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        brands.Add(
                            new Brand
                            {
                                BrandId = (int)reader[0],
                                BrandName = (string)reader[1]
                            }
                            );
                    }
                }
            }

            connection.Close();

            return brands;
        }

        public List<Brand> SelectAllBrands()
        {
            connection.Open();

            List<Brand> brands = new List<Brand>();

            using (MySqlCommand command = new MySqlCommand("select_all_brands", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        brands.Add(
                            new Brand
                            {
                                BrandId = (int)reader[0],
                                BrandName = (string)reader[1]
                            }
                            );
                    }
                }
            }

            connection.Close();

            return brands;
        }

        public Brand SelectBrandById(int? brandId)
        {
            connection.Open();

            Brand brand = null;

            using (MySqlCommand command = new MySqlCommand("select_brand_by_id", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("b_id", MySqlDbType.UInt32);
                parameter.Value = brandId;
                command.Parameters.Add(parameter);


                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        brand = new Brand()
                        {
                            BrandId = (int)reader[0],
                            BrandName = (string)reader[1]
                        };
                    }
                }
            }

            connection.Close();

            return brand;
        }


        public bool SelectBrandToCategory(int? brandId, int? categoryId)
        {
            connection.Open();

            using (MySqlCommand command = new MySqlCommand("select_brand_to_category", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;


                MySqlParameter[] parameters = new MySqlParameter[2];

                parameters[0] = new MySqlParameter("b_id", MySqlDbType.Int32);
                parameters[0].Value = brandId;

                parameters[1] = new MySqlParameter("c_id", MySqlDbType.Int32);
                parameters[1].Value = categoryId;

                command.Parameters.AddRange(parameters);


                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return true;
                    }
                }
            }

            connection.Close();

            return false;
        }
    }
}
