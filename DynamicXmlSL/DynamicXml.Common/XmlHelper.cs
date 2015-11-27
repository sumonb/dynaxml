using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DynamicXml.Common.ExtensionMethods;

namespace DynamicXml.Common
{
    public class XmlHelper
    {
        private XmlTextWriter _writer;

        private List<XmlHelperDataStoreInfo> _MultipleDataStoreInfos { get; set; }

        public XmlHelper()
        {
        }

        public void StartNew(string path, bool indentedXmlDocument)
        {
            _writer = new XmlTextWriter(path, Encoding.UTF8);

            if (indentedXmlDocument)
            {
                _writer.Formatting = Formatting.Indented;
            }


            _writer.WriteStartDocument();
        }


        public void WriteDatatableRow(DataRow prmDataRow)
        {
            //_writer.WriteStartElement("Listing");
            //foreach (var col in prmDataRow)
            //{
            //    //_writer.WriteElementString(col.ColumnName, col.ToString());
            //}
            //_writer.WriteEndElement();
        }

        public void WriteDatatable(DataTable dt, string mainElementName, string itemElementName)
        {
            _writer.WriteStartElement(mainElementName);
            foreach (DataRow row in dt.Rows)
            {
                _writer.WriteStartElement(itemElementName);
                foreach (DataColumn col in dt.Columns)
                    _writer.WriteElementString(col.ColumnName, row[col].ToString());

                _writer.WriteEndElement();
            }
            _writer.WriteEndElement();

        }

        public void WriteMultipleDatatable(List<XmlHelperDataStoreInfo> multipleDataStore)
        {
            CloseWriter();

            _MultipleDataStoreInfos = multipleDataStore;

            //start with Parent
            ProcessDataTable(0, "");
        }

        private void ProcessDataTable(int prmParentId, string prmFilterConditionValue)
        {
           

            //get the child table
            var retCurrentDt = _MultipleDataStoreInfos.Where(i => i.ParentId == prmParentId).ToList();

            //if no child table then return
            if (retCurrentDt == null || !retCurrentDt.Any())
                return;


            //all children
            foreach (var currentIndex in retCurrentDt)
            {
                //print group header flag
                bool groupMainElementPresent = !string.IsNullOrWhiteSpace(currentIndex.MainElementName);

                if (groupMainElementPresent == true)
                {
                    //starting node
                    _writer.WriteStartElement(currentIndex.MainElementName);
                }



                //currrent row
                var pid = currentIndex.RowId;

                var filtereDataTable = new DataTable();
                if (!string.IsNullOrWhiteSpace(prmFilterConditionValue))
                {
                    //filter records
                    filtereDataTable = currentIndex.Dt.FilterDataUsingCondition(string.Format("{0} = {1}", currentIndex.RelationKey, prmFilterConditionValue));
                }
                else
                {
                    //full table
                    filtereDataTable = currentIndex.Dt;
                }


                //iterate through all the records
                foreach (DataRow row in filtereDataTable.Rows)
                {

                    //get main relation key for parent
                    var filterRelationalKey = currentIndex.RelationKey;
                    var filterConditionValue = row[currentIndex.PrimaryKey].ToString();


                    //print item header flag
                    bool itemElementPresent = !string.IsNullOrWhiteSpace(currentIndex.ItemElementName);
                    if (itemElementPresent)
                    {
                        _writer.WriteStartElement(currentIndex.ItemElementName);
                    }
                   


                    foreach (DataColumn col in filtereDataTable.Columns)
                    {    //write row line item.   
                        _writer.WriteElementString(col.ColumnName, row[col].ToString());
                    }

                    //Recursive
                    //=================================================================
                    ProcessDataTable(pid, filterConditionValue);
                    //=================================================================
                    
                    //closing item header
                    if (itemElementPresent)
                    {
                        _writer.WriteEndElement();
                    }
                    
                }



                if (groupMainElementPresent == true)
                {
                    //close node
                    _writer.WriteEndElement();
                }

            }



        }

        public void CloseWriter()
        {
            if (_writer == null)
                return;
            

            _writer.WriteEndDocument();
            _writer.Flush();

            //Write the XML to file and close the writer.
            _writer.Close();
            _writer.Dispose();
        }

    }

    public class XmlHelperDataStoreInfo
    {
        public int RowId { get; set; }
        public DataTable Dt { get; set; }
        public string MainElementName { get; set; }
        public string ItemElementName { get; set; }
        public int ParentId { get; set; }
        public string PrimaryKey { get; set; }
        public string RelationKey { get; set; }

    }
}
