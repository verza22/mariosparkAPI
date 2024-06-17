using api.Models;
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

        public Customer GetCustomer(int customerID)
        {
            Customer customer = new Customer();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetCustomers", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@customerId", customerID);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                customer.Id = Convert.ToInt32(reader["KY_CUSTOMER_ID"]);
                customer.Name = reader["TX_NAME"].ToString();
                customer.Dni = reader["TX_DNI"].ToString();
                customer.Email = reader["TX_EMAIL"].ToString();
                customer.Phone = reader["TX_PHONE"].ToString();
                customer.Address = reader["TX_ADDRESS"].ToString();
                customer.StoreId = Convert.ToInt32(reader["CD_STORE_ID"]);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return customer;
        }

        public bool RemoveCustomer(int customerID)
        {
            bool isDeleted = false;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("RemoveCustomer", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@customerId", customerID);

            connection.Open();

            int result = command.ExecuteNonQuery();

            isDeleted = (result == 1);

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return isDeleted;
        }

        public int AddOrUpdateCustomer(Customer customer)
        {
            int result = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("AddOrUpdateCustomer", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@customerId", customer.Id);
            command.Parameters.AddWithValue("@name", customer.Name);
            command.Parameters.AddWithValue("@dni", customer.Dni);
            command.Parameters.AddWithValue("@email", customer.Email);
            command.Parameters.AddWithValue("@phone", customer.Phone);
            command.Parameters.AddWithValue("@address", customer.Address);
            command.Parameters.AddWithValue("@storeId", customer.StoreId);

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
