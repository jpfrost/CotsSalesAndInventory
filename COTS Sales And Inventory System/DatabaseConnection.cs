using System;
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
        public static DataSet databaseRecord = new DataSet();
        private static MySqlConnection connection;
        private static String server, userID, password,conString;

        private void ConnectToDatabase()
        {
            connection = new MySqlConnection(conString);
            var tablesList = GetDatabaseTables(connection);
            GetAllTables(tablesList,connection);
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
                databaseRecord.Tables.Add(dt);
            }
            connection.Close();
        }

        private static MySqlDataAdapter CreateDataAddapter(string query)
        {
            return new MySqlDataAdapter(query, connection);
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
            conString = "SERVER=" + server + ";" +
                        "DATABASE=" + "cotsalesinventory" + ";" +
                        "UID=" + userID + ";" +
                        "PASSWORD=" + password;
        }

        private void GetSqlSettings()
        {
            server = Properties.Settings.Default.MysqlServer;
            userID = Properties.Settings.Default.MysqlUser;
            password = Properties.Settings.Default.MysqlPass;
        }

        public static void UploadChanges()
        {
            UpdateEachTable();
        }

        private static void UpdateEachTable()
        {
            connection.Open();
            foreach (var tableName in databaseRecord.Tables)
            {
                var query = CreateSelectStatement(tableName.ToString());
                var dadapt = CreateDataAddapter(query);
                var comBuild = new MySqlCommandBuilder(dadapt);
                dadapt.Update(databaseRecord.Tables[tableName.ToString()]);
                
            }
            connection.Close();
        }
    }
}
