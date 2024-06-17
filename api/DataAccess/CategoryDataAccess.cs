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
                category.Id = Convert.ToInt32(reader["KY_CATEGORY_ID"]);
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

        public Category GetCategory(int id)
        {
            Category category = new Category();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetCategory", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@id", id);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                category.Id = Convert.ToInt32(reader["KY_CATEGORY_ID"]);
                category.Name = reader["TX_NAME"].ToString();
                category.Image = reader["TX_IMAGE"].ToString();
                category.StoreId = Convert.ToInt32(reader["CD_STORE_ID"]);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return category;
        }

        public bool RemoveCategory(int categoryId)
        {
            bool isDeleted = false;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("RemoveCategory", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@categoryId", categoryId);

            connection.Open();

            int result = command.ExecuteNonQuery();

            isDeleted = (result == 1);

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return isDeleted;
        }

        public int AddOrUpdateCategory(int categoryId, string name, string image, int storeID)
        {
            int result = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("AddOrUpdateCategory", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@categoryId", categoryId);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@image", image);
            command.Parameters.AddWithValue("@storeID", storeID);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result = Convert.ToInt32(reader["RESULT"]);
            }

            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return result;
        }
    }
}
