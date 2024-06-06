using api.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace api.DataAccess
{
    public class CategoryDataAccess
    {
        private string connectionString;

        public CategoryDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Category> GetCategoriesByStoreId(int storeID)
        {
            List<Category> categories = new List<Category>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetCategoriesByStoreId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@store_id", storeID);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Category category = new Category();
                category.CategoryId = Convert.ToInt32(reader["KY_CATEGORY_ID"]);
                category.Name = reader["TX_NAME"].ToString();
                category.Image = reader["TX_IMAGE"].ToString();
                category.StoreId = Convert.ToInt32(reader["CD_STORE_ID"]);

                categories.Add(category);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return categories;
        }
    }
}
