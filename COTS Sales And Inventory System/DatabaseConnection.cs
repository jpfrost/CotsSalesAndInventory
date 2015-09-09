﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace COTS_Sales_And_Inventory_System
{
    public class DatabaseConnection
    {
        public static DataSet DatabaseRecord = new DataSet();
        public static MySqlConnection Connection;
        private static String _server, _userId, _password,_conString;

        private void ConnectToDatabase()
        {
            Connection = new MySqlConnection(_conString);
            var tablesList = GetDatabaseTables(Connection);
            GetAllTables(tablesList,Connection);
        }

        private void GetAllTables(List<string> tablesList, MySqlConnection connection)
        {
            connection.Open();
            foreach (var tableName in tablesList)
            {
               var query= CreateSelectStatement(tableName);
                var dadapt = CreateDataAddapter(query);
                var dt = new DataTable(tableName);
                dadapt.Fill(dt);
                DatabaseRecord.Tables.Add(dt);
            }
            connection.Close();
        }

        public static DataTable GetCustomTable(string query,string tableName)
        {

           
                Connection.Open();
            
            var dadapt = CreateDataAddapter(query);
            var dt = new DataTable(tableName);
            dadapt.Fill(dt);
            Connection.Close();
            return dt;
        }

        private static MySqlDataAdapter CreateDataAddapter(string query)
        {
            return new MySqlDataAdapter(query, Connection);
        }

        private static string CreateSelectStatement(string tableName)
        {
            string query = "Select * from " + tableName + ";";
            return query;
        }

        private List<String> GetDatabaseTables(MySqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "Show Tables;";
            connection.Open();
            var reader = command.ExecuteReader();
            var list = new List<String>();
            while (reader.Read())
            {
                for (int x = 0; x < reader.FieldCount; x++)
                {
                    list.Add(reader.GetValue(x).ToString());
                }
                
            }
            connection.Close();
            return list;
        }

        public DatabaseConnection()
        {
            GetSqlSettings();
            ConnectionString();
            ConnectToDatabase();
        }

        private void ConnectionString()
        {
            _conString = "SERVER=" + _server + ";" +
                        "DATABASE=" + "cotsalesinventory" + ";" +
                        "UID=" + _userId + ";" +
                        "PASSWORD=" + _password;
        }

        private void GetSqlSettings()
        {
            _server = Properties.Settings.Default.MysqlServer;
            _userId = Properties.Settings.Default.MysqlUser;
            _password = Properties.Settings.Default.MysqlPass;
        }

        public static void UploadChanges()
        {
            UpdateEachTable();
        }

        private static void UpdateEachTable()
        {
            Connection.Open();
            foreach (var tableName in DatabaseRecord.Tables)
            {
                UpdateTable(tableName);
            }
            Connection.Close();
        }

        public static void UpdateTable(object tableName)
        {

            try
            {
                var query = CreateSelectStatement(tableName.ToString());
                var dadapt = CreateDataAddapter(query);
                var comBuild = new MySqlCommandBuilder(dadapt);
                dadapt.Update(DatabaseRecord.Tables[tableName.ToString()]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
