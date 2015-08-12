using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;

namespace COTS_Sales_And_Inventory_System
{
    public class DatabaseConnection
    {
        DataSet databaseRecord = new DataSet();
        private String server, userID, password;

        public void ConnectToDatabase()
        {
            
        }

        public DatabaseConnection()
        {
            GetSqlSettings();
        }

        private void GetSqlSettings()
        {
            server = Properties.Settings.Default.MysqlServer;
            userID = Properties.Settings.Default.MysqlUser;
            password = Properties.Settings.Default.MysqlPass;
        }
    }
}
