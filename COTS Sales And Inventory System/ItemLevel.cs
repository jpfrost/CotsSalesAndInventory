using System;
using System.Data;
using System.Linq;

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

        private int FindMinCriticalLevel()
        {
            return _orderQty < 20 ? Convert.ToInt32(_orderQty*.2) : Convert.ToInt32(_orderQty*.1);
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