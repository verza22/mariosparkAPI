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

        public bool AddOrUpdateHotelRoom(HotelRoom room)
        {
            bool isAdded = false;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("AddOrUpdateHotelRoom", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@roomId", room.Id);
            command.Parameters.AddWithValue("@roomName", room.Name);
            command.Parameters.AddWithValue("@capacity", room.Capacity);
            command.Parameters.AddWithValue("@roomTypeId", room.Type);
            command.Parameters.AddWithValue("@storeId", room.StoreId);

            connection.Open();

            int result = command.ExecuteNonQuery();
            isAdded = (result > 0);

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return isAdded;
        }
    }
}
