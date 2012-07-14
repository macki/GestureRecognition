using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace MackiTools.MackiTools.DataGridViewUtil
{
    public class DataGridViewUtil
    {
        /// <summary>
        /// Get column with specified name
        /// </summary>
        /// <param name="DataGridView"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static DataGridViewTextBoxColumn GetColumn(DataGridView DataGridView, string columnName)
        {
            foreach(DataGridViewTextBoxColumn column in DataGridView.Columns)
            {
                if (column.Name == columnName)
                {
                    return column;
                }
            }
             return null;
        }

        /// <summary>
        /// Set properties of given column
        /// </summary>
        /// properties format [properties:value,properties:value]
        /// <param name="propertiesName"></param>
        public static void SetColumn(DataGridViewTextBoxColumn column, string propertiesPairs)
        {
            var propertiesTable = propertiesPairs.Split(',');
            Type columnType = column.GetType();
            foreach (PropertyInfo propertyInfo in columnType.GetProperties() )
            {
                for(int i = 0; i < propertiesTable.Length; i++)
                {
                    var pairNameValue = propertiesTable[i].Split(':');
                    if (propertyInfo.Name ==  pairNameValue[0])
                    {
                        propertyInfo.SetValue(column, Convert.ChangeType(pairNameValue[1], propertyInfo.PropertyType), null);
                    }
                }
            }
        }

    }
}
