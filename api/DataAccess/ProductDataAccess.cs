using api.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace api.DataAccess
{
    public class ProductDataAccess
    {
        private string connectionString;

        public ProductDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Product> GetProducts(int storeID, int categoryID)
        {
            List<Product> products = new List<Product>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetProducts", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@store_id", storeID);
            command.Parameters.AddWithValue("@category_id", categoryID);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product product = new Product();

                product.Id = Convert.ToInt32(reader["KY_PRODUCT_ID"]);
                product.Name = reader["TX_NAME"].ToString();
                product.Description = reader["TX_DESCRIPTION"].ToString();
                product.Price = Convert.ToDecimal(reader["DB_PRICE"]);
                product.CategoryId = Convert.ToInt32(reader["CD_CATEGORY_ID"]);
                product.Image = reader["TX_IMAGE"].ToString();
                product.StoreId = Convert.ToInt32(reader["CD_STORE_ID"]);

                products.Add(product);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return products;
        }
    }
}
