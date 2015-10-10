using System;
using System.Data;
using System.Linq;
using COTS_Sales_And_Inventory_System.Properties;

namespace COTS_Sales_And_Inventory_System
{
    public class ItemLevel
    {
        private readonly int _orderQty;
        private readonly int _sizeId;

        public ItemLevel(int sizeId, int orderQty)
        {
            _sizeId = sizeId;
            _orderQty = orderQty;
        }

        public ItemLevel()
        {

        }

        public void InsertCriticalLevel()
        {
            try
            {
                var found = DatabaseConnection.DatabaseRecord.Tables["itemLevel"].Select("sizeID='"
                                                                                         + _sizeId + "'");
                if (found.Length >= 0)
                {
                    var itemLevelId = GetCurrentCount("itemlevel", "itemLevelId");
                    var minLevel = FindMinCriticalLevel();
                    var newLevel = DatabaseConnection.DatabaseRecord.Tables["itemLevel"].NewRow();
                    newLevel["itemLevelId"] = itemLevelId;
                    newLevel["SizeID"] = _sizeId;
                    newLevel["itemMinLevel"] = minLevel;
                    newLevel["itemMaxLevel"] = _orderQty;
                    DatabaseConnection.DatabaseRecord.Tables["itemlevel"].Rows.Add(newLevel);
                    DatabaseConnection.UploadChanges();
                }
                else
                {
                    found[0]["itemMinLevel"] = FindMinCriticalLevel();
                    found[0]["itemMaxLevel"] = _orderQty;
                    DatabaseConnection.UploadChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private int? FindMinCriticalLevel()
        {
            try
            {
                var x = Convert.ToDouble(Settings.Default.critLowLevel/100);
                var y = Convert.ToDouble(Settings.Default.critHighLevel/100);
                return _orderQty < Settings.Default.CritMedian ? Convert.ToInt32(_orderQty*x) : Convert.ToInt32(_orderQty*y);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        private int GetCurrentCount(string tableName, string columbName)
        {
            var value = 0;
            if (DatabaseConnection.DatabaseRecord.Tables[tableName].Rows.Count == 0)
            {
                return 1;
            }
            value =
                (from DataRow rows in DatabaseConnection.DatabaseRecord.Tables[tableName].Rows
                    select (int) rows[columbName]).Concat(new[] {value}).Max();
            return value + 1;
        }
    }
}