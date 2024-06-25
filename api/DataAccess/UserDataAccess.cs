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

        public bool RemoveUser(int userID)
        {
            bool isDeleted = false;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("RemoveUser", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@userId", userID);

            connection.Open();

            int result = command.ExecuteNonQuery();

            isDeleted = (result > 0);

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return isDeleted;
        }

        public int AddOrUpdateUser(User user)
        {
            int result = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("AddOrUpdateUser", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@userId", user.Id);
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@userTypeId", user.Type);
            command.Parameters.AddWithValue("@storeId", user.DefaultStoreID);

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

        public int UpdateUserToken(int userID, string token)
        {
            int result = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UpdateUserToken", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@userId", userID);
            command.Parameters.AddWithValue("@token", token);

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

        public List<string> GetUserTokenByStore(int storeID)
        {
            List<string> tokenList = new List<string>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetUserTokenByStore", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@storeID", storeID);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tokenList.Add(reader["TX_FCM_TOKEN"].ToString());
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return tokenList;
        }
    }
}
