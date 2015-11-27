using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DynamicXml.Common;
using DynamicXml.Common.ExtensionMethods;

namespace DynamicXml.Bll
{
    public class Processor
    {
        private XmlWritterExtended _writer;

        private List<PacketConfigurationInfo> _MultipleDataStoreInfos { get; set; }


        public void StartNew(string path, bool indentedXmlDocument)
        {
            _writer = new XmlWritterExtended(path, Encoding.UTF8);

            if (indentedXmlDocument)
            {
                _writer.Formatting = Formatting.Indented;
            }


            _writer.WriteStartDocument();
        }




        public void WriteMultipleDatatable(List<PacketConfigurationInfo> multipleDataStore)
        {


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


            //all children datatable
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
   
                    //now write all elements and attribute from current row
                    _writer.WriteSmartElement(currentIndex.ItemElementName, row, filtereDataTable, currentIndex.XmlNodeMetaInfo);
      

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
                    //close node main
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


}
