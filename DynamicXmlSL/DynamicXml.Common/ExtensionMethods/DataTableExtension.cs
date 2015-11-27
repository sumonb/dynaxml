using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicXml.Common.ExtensionMethods
{
    public static class DataTableExtension
    {
        public static DataTable FilterDataUsingCondition(this DataTable dt, string filterCondition)
        {
            var view = dt.DefaultView;
            /////view.Sort = "LastName, FirstName";
            view.RowFilter = filterCondition;

            var retDt = view.ToTable();
            return retDt;
        }
    }
}
