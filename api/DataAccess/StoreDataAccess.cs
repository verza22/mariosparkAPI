using api.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace api.DataAccess
{
    public class StoreDataAccess
    {
        private string connectionString;

        public StoreDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Store> GetStoresByOwnerId(int ownerId)
        {
            List<Store> stores = new List<Store>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetStoresByOwnerId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@owner_id", ownerId);

            // Abre la conexión manualmente
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Store store = new Store();
                store.StoreId = Convert.ToInt32(reader["KY_STORE_ID"]);
                store.StoreName = reader["TX_STORE_NAME"].ToString();
                store.StoreAddress = reader["TX_STORE_ADDRESS"].ToString();
                store.OwnerId = Convert.ToInt32(reader["CD_OWNER_ID"]);

                stores.Add(store);
            }
            reader.Close();

            // Cierra la conexión manualmente
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            return stores;
        }
    }
}
