﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using MySql.Data.MySqlClient;

namespace COTS_Sales_And_Inventory_System
{
    public class DatabaseConnection
    {
        public static DataSet DatabaseRecord = new DataSet();
        public static MySqlConnection Connection;
        private static String _server, _userId, _password, _conString;

        public DatabaseConnection()
        {
            GetSqlSettings();
            ConnectionString();
            ConnectToDatabase();
        }

        private void ConnectToDatabase()
        {
            Connection = new MySqlConnection(_conString);
            var tablesList = GetDatabaseTables(Connection);
            GetAllTables(tablesList, Connection);
        }

        private static void GetAllTables(List<string> tablesList, MySqlConnection connection)
        {
            DatabaseRecord.Tables.Clear();
            connection.Open();
            foreach (var tableName in tablesList)
            {
                var query = CreateSelectStatement(tableName);
                var dadapt = CreateDataAddapter(query);
                var dt = new DataTable(tableName);
                dadapt.Fill(dt);
                DatabaseRecord.Tables.Add(dt);
            }
            connection.Close();
        }

        public static DataTable GetCustomTable(string query, string tableName)
        {
            
            if (!Connection.State.ToString().Equals("Open"))
            {
                Connection.Open();
            }
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

        public static string CreateSelectStatement(string tableName)
        {
            var query = "Select * from " + tableName + ";";
            return query;
        }

        private static List<String> GetDatabaseTables(MySqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "Show Tables;";
            if (!connection.State.ToString().Equals("Open"))
            {
                connection.Open();
            }
            var reader = command.ExecuteReader();
            var list = new List<String>();
            while (reader.Read())
            {
                for (var x = 0; x < reader.FieldCount; x++)
                {
                    list.Add(reader.GetValue(x).ToString());
                }
            }
            connection.Close();
            return list;
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
            DropTableAndRetreive();
        }

        private static void DropTableAndRetreive()
        {
            var tablesList = GetDatabaseTables(Connection);
            DropTables(tablesList, Connection);
            GetAllTables(tablesList, Connection);
        }

        private static void DropTables(List<string> tablesList, MySqlConnection connection)
        {
            foreach (var tableName in tablesList)
            {
                DatabaseRecord.Tables.Remove(tableName);
            }
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

        public static void TruncateDatabase()
        {
            
            if (!Connection.State.ToString().Equals("Open"))
            {
                Connection.Open();
            }
            var tableList = GetDatabaseTables(Connection);
            foreach (var cmd in tableList.Select(tableName => "TRUNCATE TABLE " + tableName).Select(query => new MySqlCommand(query, Connection)))
            {
                cmd.ExecuteNonQuery();
            }
            Connection.Close();
        }

        public static void RecreateMysqlDatabase()
        {
            if (!Connection.State.ToString().Equals("Open"))
            {
                Connection.Open();
            }
            var runScript = new MySqlScript(Connection, Properties.Settings.Default.WipeDatabase);
            runScript.Execute();
            Connection.Close();
        }
    }
}