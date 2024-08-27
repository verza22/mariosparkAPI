using api.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace api.DataAccess
{
    public class AuthDataAccess
    {
        private string connectionString;

        public AuthDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public User Login(string userName, string password)
        {
            User user = new User();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Login", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@Password", password);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                user.Id = Convert.ToInt32(reader["KY_USER_ID"]);
                user.Username = reader["TX_USERNAME"].ToString();
                user.Name = reader["TX_NAME"].ToString();
                user.Type = Convert.ToInt32(reader["CD_USER_TYPE_ID"]);
                user.DefaultStoreID = Convert.ToInt32(reader["DEFAULT_STORE_ID"]);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return user;
        }

        public List<UserType> GetUserTypes()
        {
            List<UserType> userTypes = new List<UserType>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetUserTypes", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                UserType userType = new UserType();

                userType.Id = Convert.ToInt32(reader["KY_USER_TYPE_ID"]);
                userType.Name = reader["TX_USER_TYPE_NAME"].ToString();

                userTypes.Add(userType);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return userTypes;
        }

        public List<OrderStatus> GetOrderStatus()
        {
            List<OrderStatus> orderStatusList = new List<OrderStatus>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetOrderStatus", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                OrderStatus orderStatus = new OrderStatus();

                orderStatus.Id = Convert.ToInt32(reader["KY_ORDER_STATUS_ID"]);
                orderStatus.Name = reader["TX_STATUS_NAME"].ToString();

                orderStatusList.Add(orderStatus);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return orderStatusList;
        }

        public List<HotelOrderType> GetHotelOrderTypes()
        {
            List<HotelOrderType> hotelOrderTypeList = new List<HotelOrderType>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetHotelOrderTypes", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HotelOrderType hotelOrderType = new HotelOrderType();

                hotelOrderType.Id = Convert.ToInt32(reader["KY_HOTEL_ORDER_TYPE_ID"]);
                hotelOrderType.Name = reader["TX_ORDER_TYPE_NAME"].ToString();

                hotelOrderTypeList.Add(hotelOrderType);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return hotelOrderTypeList;
        }

        public List<HotelRoomType> GetHotelRoomTypes(int store_id)
        {
            List<HotelRoomType> hotelRoomTypeList = new List<HotelRoomType>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetHotelRoomTypes", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@store_id", store_id);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HotelRoomType hotelRoomType = new HotelRoomType();

                hotelRoomType.Id = Convert.ToInt32(reader["KY_ROOM_TYPE_ID"]);
                hotelRoomType.Name = reader["TX_TYPE_NAME"].ToString();
                hotelRoomType.StoreId = Convert.ToInt32(reader["CD_STORE_ID"]);

                hotelRoomTypeList.Add(hotelRoomType);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return hotelRoomTypeList;
        }

        public List<UserConfig> GetUserConfig(int userID)
        {
            List<UserConfig> userConfigList = new List<UserConfig>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetUserConfig", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@userID", userID);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                UserConfig userConfig = new UserConfig();

                userConfig.Id = Convert.ToInt32(reader["KY_CONFIG_ID"]);
                userConfig.Code = reader["TX_CODE"].ToString();
                userConfig.Value = reader["TX_VALUE"].ToString();

                userConfigList.Add(userConfig);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return userConfigList;
        }
    }
}
