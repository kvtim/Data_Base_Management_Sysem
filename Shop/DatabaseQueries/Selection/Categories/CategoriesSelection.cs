using MySql.Data.MySqlClient;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.DatabaseQueries.Selection.Categories
{
    public class CategoriesSelection
    {
        MySqlConnection connection;

        public CategoriesSelection()
        {
            connection = new MySqlConnection("server=localhost;Database=mydb;user=root;password=admin");
        }

        public List<Category> SelectAllCategories()
        {
            connection.Open();

            List<Category> categories = new List<Category>();

            MySqlCommand command = new MySqlCommand("select_all_categories", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    categories.Add(
                        new Category
                        {
                            CategoryId = (int)reader[0],
                            CategoryName = (string)reader[1]
                        }
                        );
                }
            }

            connection.Close();

            return categories;
        }

        public Category SelectCategoryById(int? categoryId)
        {
            connection.Open();

            Category category = null;

            using (MySqlCommand command = new MySqlCommand("select_category_by_id", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("c_id", MySqlDbType.UInt32);
                parameter.Value = categoryId;
                command.Parameters.Add(parameter);


                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        category = new Category()
                        {
                            CategoryId = (int)reader[0],
                            CategoryName = (string)reader[1]
                        };
                    }
                }
            }

            connection.Close();

            return category;
        }

        public List<Category> SelectCategoriesOfTheSameBrand(int? brandId)
        {
            connection.Open();

            List<Category> categories = new List<Category>();

            using (MySqlCommand command = new MySqlCommand("select_categories_of_the_same_brand", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                MySqlParameter parameter = new MySqlParameter("b_id", MySqlDbType.Int32);
                parameter.Value = brandId;
                command.Parameters.Add(parameter);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(
                            new Category
                            {
                                CategoryId = (int)reader[0],
                                CategoryName = (string)reader[1]
                            }
                            );
                    }
                }
            }

            connection.Close();

            return categories;
        }
    }
}
