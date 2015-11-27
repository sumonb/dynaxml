using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicXml.Common.ExtensionMethods
{
    public static class DataRowExtension
    {
        public static DataTable GenerateTable(this DataRow dr, DataTable fromDataTable)
        {
            var newDt = fromDataTable.Clone();
            newDt.ImportRow(dr);
            return newDt;
        }
    }
}
