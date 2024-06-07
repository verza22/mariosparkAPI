using api.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace api.DataAccess
{
    public class OrderDataAccess
    {
        private string connectionString;

        public OrderDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Order> GetOrders(int store_id)
        {
            List<Order> orders = new List<Order>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetOrders", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@store_id", store_id);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Order order = new Order();

                order.Id = Convert.ToInt32(reader["KY_ORDER_ID"]);
                order.CashierId = Convert.ToInt32(reader["CD_CASHIER_ID"]);
                order.TableNumber = Convert.ToInt32(reader["CD_TABLE_NUMBER"]);
                order.WaiterId = Convert.ToInt32(reader["CD_WAITER_ID"]);
                order.ChefId = Convert.ToInt32(reader["CD_CHEF_ID"]);
                order.Total = Convert.ToDecimal(reader["CD_TOTAL"]);
                order.Date = Convert.ToDateTime(reader["DT_DATE"]);
                order.PaymentMethod = reader["TX_PAYMENT_METHOD"].ToString();
                order.OrderStatusId = Convert.ToInt32(reader["CD_ORDER_STATUS"]);
                order.StoreId = Convert.ToInt32(reader["CD_STORE_ID"]);
                order.Customer = JsonConvert.DeserializeObject<Customer>(reader["JS_CUSTOMER"].ToString());
                order.Products = JsonConvert.DeserializeObject<List<Product>>(reader["JS_PRODUCTS"].ToString());

                orders.Add(order);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return orders;
        }
    }
}
