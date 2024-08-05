using api.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace api.DataAccess
{
    public class PrinterDataAccess
    {
        private string connectionString;

        public PrinterDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Printer> GetPrinters(int storeID)
        {
            List<Printer> printers = new List<Printer>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetPrinters", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@storeID", storeID);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Printer printer = new Printer();

                printer.Id = Convert.ToInt32(reader["KY_PRINTER_ID"]);
                printer.Name = reader["TX_NAME"].ToString();
                printer.Ip = reader["TX_IP"].ToString();
                printer.IsPrincipal = Convert.ToBoolean(reader["CD_IS_PRINCIPAL"]);
                printer.StoreID = Convert.ToInt32(reader["CD_STORE_ID"]);

                printers.Add(printer);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return printers;
        }

        public bool RemovePrinter(int printerID)
        {
            bool isDeleted = false;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("RemovePrinter", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@printerID", printerID);

            connection.Open();

            int result = command.ExecuteNonQuery();

            isDeleted = (result > 0);

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return isDeleted;
        }

        public int AddOrUpdatePrinter(Printer printer)
        {
            int result = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("AddOrUpdatePrinter", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@printerID", printer.Id);
            command.Parameters.AddWithValue("@name", printer.Name);
            command.Parameters.AddWithValue("@ip", printer.Ip);
            command.Parameters.AddWithValue("@isPrincipal", printer.IsPrincipal);
            command.Parameters.AddWithValue("@storeId", printer.StoreID);

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
