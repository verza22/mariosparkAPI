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
                user.UserId = Convert.ToInt32(reader["KY_USER_ID"]);
                user.Username = reader["TX_USERNAME"].ToString();
                user.Name = reader["TX_NAME"].ToString();
                user.UserTypeId = Convert.ToInt32(reader["CD_USER_TYPE_ID"]);
                user.OwnerId = Convert.ToInt32(reader["CD_OWNER_ID"]);
                user.DefaultStoreID = Convert.ToInt32(reader["DEFAULT_STORE_ID"]);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return user;
        }
    }
}
