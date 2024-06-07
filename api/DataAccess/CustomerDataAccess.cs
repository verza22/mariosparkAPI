﻿using api.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace api.DataAccess
{
    public class CustomerDataAccess
    {
        private string connectionString;

        public CustomerDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Customer> GetCustomers(int storeID)
        {
            List<Customer> customers = new List<Customer>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetCustomers", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@store_id", storeID);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Customer customer = new Customer();

                customer.Id = Convert.ToInt32(reader["KY_CUSTOMER_ID"]);
                customer.Name = reader["TX_NAME"].ToString();
                customer.Dni = reader["TX_DNI"].ToString();
                customer.Email = reader["TX_EMAIL"].ToString();
                customer.Phone = reader["TX_PHONE"].ToString();
                customer.Address = reader["TX_ADDRESS"].ToString();
                customer.StoreId = Convert.ToInt32(reader["CD_STORE_ID"]);

                customers.Add(customer);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return customers;
        }
    }
}
