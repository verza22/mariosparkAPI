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
    }
}
