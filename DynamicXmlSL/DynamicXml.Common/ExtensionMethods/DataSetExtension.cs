using System;
using System.Data;


namespace DynamicXml.Common.ExtensionMethods
{
    public static class DataSetExtension
    {
        public static void MakeUnspecified(this DataSet ds)
        {
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataColumn dc in dt.Columns)
                {

                    if (dc.DataType == typeof(DateTime))
                    {
                        dc.DateTimeMode = DataSetDateTime.Unspecified;
                    }
                }
            }
        }

    }
}
