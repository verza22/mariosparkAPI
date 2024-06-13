using api.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace api.DataAccess
{
    public class HotelOrderDataAccess
    {
        private string connectionString;

        public HotelOrderDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<HotelOrder> GetHotelOrders(int store_id)
        {
            List<HotelOrder> orders = new List<HotelOrder>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetHotelOrders", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@store_id", store_id);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HotelOrder order = new HotelOrder();

                order.Id = Convert.ToInt32(reader["KY_ORDER_ID"]);
                order.UserId = Convert.ToInt32(reader["CD_USER_ID"]);
                order.Total = Convert.ToDecimal(reader["DEC_TOTAL"]);
                order.DateIn = Convert.ToDateTime(reader["DT_DATE_IN"]);
                order.DateOut = Convert.ToDateTime(reader["DT_DATE_OUT"]);
                order.PaymentMethod = reader["TX_PAYMENT_METHOD"].ToString();
                order.People = Convert.ToInt32(reader["INT_PEOPLE"]);
                order.StoreId = Convert.ToInt32(reader["CD_STORE_ID"]);
                order.Room = JsonConvert.DeserializeObject<HotelRoom>(reader["JS_ROOM"].ToString());
                order.Customer = JsonConvert.DeserializeObject<Customer>(reader["JS_CUSTOMER"].ToString());

                orders.Add(order);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return orders;
        }

        public bool RemoveHotelOrder(int orderID)
        {
            bool isDeleted = false;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("RemoveHotelOrder", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@orderId", orderID);

            connection.Open();

            int result = command.ExecuteNonQuery();

            isDeleted = (result == 1);

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return isDeleted;
        }

        public bool AddOrUpdateHotelOrder(HotelOrder order)
        {
            bool isAdded = false;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("AddOrUpdateHotelOrder", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@orderId", order.Id);
            command.Parameters.AddWithValue("@userId", order.UserId);
            command.Parameters.AddWithValue("@total", order.Total);
            command.Parameters.AddWithValue("@dateIn", order.DateIn);
            command.Parameters.AddWithValue("@dateOut", order.DateOut);
            command.Parameters.AddWithValue("@paymentMethod", order.PaymentMethod);
            command.Parameters.AddWithValue("@people", order.People);
            command.Parameters.AddWithValue("@storeId", order.StoreId);
            command.Parameters.AddWithValue("@customer", JsonConvert.SerializeObject(order.Customer));
            command.Parameters.AddWithValue("@room", JsonConvert.SerializeObject(order.Room));

            connection.Open();

            int result = command.ExecuteNonQuery();
            isAdded = (result > 0);

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return isAdded;
        }
    }
}
