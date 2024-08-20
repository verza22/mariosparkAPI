using api.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace api.DataAccess
{
    public class HotelRoomDataAccess
    {
        private string connectionString;

        public HotelRoomDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public HotelRoom GetHotelRoom(int id)
        {
            HotelRoom room = new HotelRoom();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetHotelRoom", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@id", id);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                room.Id = Convert.ToInt32(reader["KY_ROOM_ID"]);
                room.Name = reader["TX_ROOM_NAME"].ToString();
                room.Capacity = Convert.ToInt32(reader["INT_CAPACITY"]);
                room.Type = Convert.ToInt32(reader["CD_ROOM_TYPE_ID"].ToString());
                room.StoreId = Convert.ToInt32(reader["CD_STORE_ID"]);
                room.PriceBabies = Convert.ToDecimal(reader["DEC_PRICE_BABIES"]);
                room.PriceChildren = Convert.ToDecimal(reader["DEC_PRICE_CHILDREN"]);
                room.PriceAdults = Convert.ToDecimal(reader["DEC_PRICE_ADULTS"]);
                room.Image = reader["TX_IMAGE"].ToString();
                room.Description = reader["TX_DESCRIPTION"].ToString();
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return room;
        }

        public List<HotelRoom> GetHotelRooms(int store_id)
        {
            List<HotelRoom> rooms = new List<HotelRoom>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetHotelRooms", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@store_id", store_id);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HotelRoom room = new HotelRoom();

                room.Id = Convert.ToInt32(reader["KY_ROOM_ID"]);
                room.Name = reader["TX_ROOM_NAME"].ToString();
                room.Capacity = Convert.ToInt32(reader["INT_CAPACITY"]);
                room.Type = Convert.ToInt32(reader["CD_ROOM_TYPE_ID"].ToString());
                room.StoreId = Convert.ToInt32(reader["CD_STORE_ID"]);
                room.PriceBabies = Convert.ToDecimal(reader["DEC_PRICE_BABIES"]);
                room.PriceChildren = Convert.ToDecimal(reader["DEC_PRICE_CHILDREN"]);
                room.PriceAdults = Convert.ToDecimal(reader["DEC_PRICE_ADULTS"]);
                room.Image = reader["TX_IMAGE"].ToString();
                room.Description = reader["TX_DESCRIPTION"].ToString();

                rooms.Add(room);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return rooms;
        }

        public bool RemoveHotelRoom(int roomID)
        {
            bool isDeleted = false;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("RemoveHotelRoom", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@roomId", roomID);

            connection.Open();

            int result = command.ExecuteNonQuery();

            isDeleted = (result == 1);

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return isDeleted;
        }

        public int AddOrUpdateHotelRoom(HotelRoom room)
        {
            int result = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("AddOrUpdateHotelRoom", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@roomId", room.Id);
            command.Parameters.AddWithValue("@roomName", room.Name);
            command.Parameters.AddWithValue("@capacity", room.Capacity);
            command.Parameters.AddWithValue("@roomTypeId", room.Type);
            command.Parameters.AddWithValue("@storeId", room.StoreId);
            command.Parameters.AddWithValue("@priceBabies", room.PriceBabies);
            command.Parameters.AddWithValue("@priceChildren", room.PriceChildren);
            command.Parameters.AddWithValue("@priceAdults", room.PriceAdults);
            command.Parameters.AddWithValue("@image", room.Image);
            command.Parameters.AddWithValue("@description", room.Description);

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
