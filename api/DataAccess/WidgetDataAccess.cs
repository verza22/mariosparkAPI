using api.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace api.DataAccess
{
    public class WidgetDataAccess
    {
        private string connectionString;

        public WidgetDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Widget> GetWidgets(int userID)
        {
            List<Widget> widgets = new List<Widget>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetWidgets", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@userId", userID);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Widget widget = new Widget();

                widget.Id = Convert.ToInt32(reader["KY_WIDGET_ID"]);
                widget.UserID = Convert.ToInt32(reader["CD_USER_ID"]);
                widget.Title = reader["TX_TITLE"].ToString();
                widget.Symbol = reader["TX_SYMBOL"].ToString();
                widget.IsLeading = Convert.ToBoolean(reader["CD_IS_LEADING"]);
                widget.InfoType = (WidgetInfoType) Convert.ToInt32(reader["CD_INFO_TYPE"]);
                widget.Type = (WidgetType) Convert.ToInt32(reader["CD_TYPE"]);
                widget.DateFrom = reader["TX_DATE_FROM"].ToString();
                widget.DateTo = reader["TX_DATE_TO"].ToString();
                widget.DateFromType = (DateType) Convert.ToInt32(reader["CD_DATE_FROM_TYPE"]);
                widget.DateToType = (DateType) Convert.ToInt32(reader["CD_DATE_TO_TYPE"]);
                widget.Position = Convert.ToInt32(reader["CD_POSITION"]);
                widget.SizeX = Convert.ToInt32(reader["CD_SIZEX"]);
                widget.SizeY = Convert.ToInt32(reader["CD_SIZEY"]);
                widget.BgColor = reader["TX_BGCOLOR"].ToString();

                widgets.Add(widget);
            }
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return widgets;
        }

        public bool RemoveWidget(int widgetId)
        {
            bool isDeleted = false;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("RemoveWidget", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@widgetId", widgetId);

            connection.Open();

            int result = command.ExecuteNonQuery();

            isDeleted = (result == 1);

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return isDeleted;
        }

        public int AddOrUpdateWidget(Widget widget)
        {
            int result = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("AddOrUpdateWidget", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@widgetId", widget.Id);
            command.Parameters.AddWithValue("@userId", widget.UserID);
            command.Parameters.AddWithValue("@title", widget.Title);
            command.Parameters.AddWithValue("@symbol", widget.Symbol);
            command.Parameters.AddWithValue("@isLeading", widget.IsLeading);
            command.Parameters.AddWithValue("@infoType", widget.InfoType);
            command.Parameters.AddWithValue("@type", widget.Type);
            command.Parameters.AddWithValue("@dateFrom", widget.DateFrom);
            command.Parameters.AddWithValue("@dateTo", widget.DateTo);
            command.Parameters.AddWithValue("@dateFromType", widget.DateFromType);
            command.Parameters.AddWithValue("@dateToType", widget.DateToType);
            command.Parameters.AddWithValue("@position", widget.Position);
            command.Parameters.AddWithValue("@sizeX", widget.SizeX);
            command.Parameters.AddWithValue("@sizeY", widget.SizeY);
            command.Parameters.AddWithValue("@bgColor", widget.BgColor);

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

        public int GetWidgetData(int widgetID, int storeID)
        {
            int result = 0;

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetWidgetData", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@widgetID", widgetID);
            command.Parameters.AddWithValue("@storeID", storeID);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result = reader["RESULT"] != DBNull.Value ? Convert.ToInt32(reader["RESULT"]) : 0;
            }

            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return result;
        }

        public DataTable GetWidgetDataList(int widgetID, int storeID)
        {
            DataTable resultTable = new DataTable();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetWidgetDataList", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@widgetID", widgetID);
            command.Parameters.AddWithValue("@storeID", storeID);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            resultTable.Load(reader);
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return resultTable;
        }

        public void UpdateWidgetPositions(int userID, string widgetIDs)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UpdateWidgetPositions", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@widgetIDs", widgetIDs);

            connection.Open();
            command.ExecuteNonQuery();

            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

        public DataTable GetWidgetTypeList()
        {
            DataTable resultTable = new DataTable();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetWidgetTypeList", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            resultTable.Load(reader);
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return resultTable;
        }

        public DataTable GetWidgetInfoTypeList()
        {
            DataTable resultTable = new DataTable();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetWidgetInfoTypeList", connection);
            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            resultTable.Load(reader);
            reader.Close();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            return resultTable;
        }

    }
}