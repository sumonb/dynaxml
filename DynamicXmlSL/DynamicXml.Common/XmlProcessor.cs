using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicXml.Common
{
    public class XmlProcessor
    {
        private readonly DataSet _dsMain;
        public XmlProcessor(string rootName)
        {
            _dsMain = new DataSet(rootName);
        }

        public void AddTable(DataTable dt)
        {
            _dsMain.Tables.Add(dt);
        }
        public void CreateRelationship(DataTable dt)
        {
            _dsMain.Tables.Add(dt);
        }


        private void PrepareTablesWithNoRows()
        {

            foreach (DataTable table in _dsMain.Tables)
            {
                if (table.Rows.Count == 0)
                {
                    table.Rows.Add(table.NewRow());
                }
            }

        }

    }

    //public class XmlProcessorTableInfo
    //{
    //    public string TableName { get; set; }
    //    public string TableName { get; set; }
    //}
}
