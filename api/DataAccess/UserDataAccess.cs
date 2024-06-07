using api.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace api.DataAccess
{
    public class UserDataAccess
    {
        private string connectionString;

        public UserDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<User> GetUsers(int store_id)
        {
            List<User> users = new List<User>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetUsers", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@store_id", store_id);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                User user = new User();

                user.Id = Convert.ToInt32(reader["KY_USER_ID"]);
                user.Username = reader["TX_USERNAME"].ToString();
                user.Name = reader["TX_NAME"].ToString();
                user.Type = Convert.ToInt32(reader["CD_USER_TYPE_ID"]);

                users.Add(user);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return users;
        }
    }
}
