using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DynamicXml.Common
{
    public class XmlWritterExtended : XmlTextWriter
    {
        public XmlWritterExtended(string filename, Encoding encoding)
            : base(filename, encoding)
        {

        }

        public void WriteSmartElement(DataRow prmCurrentRow, DataTable dt)
        {
            foreach (DataColumn col in dt.Columns)
            {
                base.WriteElementString(col.ColumnName, prmCurrentRow[col].ToString());
            }
        }

        public void WriteDatatable(DataTable dt, string mainElementName, string itemElementName)
        {
            base.WriteStartElement(mainElementName);
            foreach (DataRow row in dt.Rows)
            {
                base.WriteStartElement(itemElementName);
                foreach (DataColumn col in dt.Columns)
                    base.WriteElementString(col.ColumnName, row[col].ToString());

                base.WriteEndElement();
            }
            base.WriteEndElement();

        }


        public void WriteSmartElement(string prmHeaderElementName, DataRow prmCurrentRow, DataTable dt, List<XmlElementMetaInfo> prmAttributeCollection)
        {
            var itemElementPresent = !string.IsNullOrWhiteSpace(prmHeaderElementName);
            if (itemElementPresent)
            {
                base.WriteStartElement(prmHeaderElementName);
            }

            //now print attribute
            foreach (var currentAttribute in prmAttributeCollection.Where(a=>a.AttributeType==true))
            {
                base.WriteAttributeString(currentAttribute.Name, prmCurrentRow[currentAttribute.Name].ToString());
            }

            
            foreach (DataColumn col in dt.Columns)
            {
                //don't write element if were part of attribute
                var ret = prmAttributeCollection.Count(i => i.Name == col.ColumnName);
                if(ret > 0)
                    continue;


                base.WriteElementString(col.ColumnName, prmCurrentRow[col].ToString());
            }
        }


        public void WriteSmartTag(string prmName, string prmValue, bool prmElementType = true)
        {
            if (prmElementType)
            {

                base.WriteElementString(prmName, prmValue);
            }
            else
            {
                //attribute
                base.WriteAttributeString(prmName, prmValue);
            }

        }
    }

    public class XmlElementMetaInfo
    {
        public string Name { get; set; }
        public bool AttributeType { get; set; }
        public string CustomFormatter { get; set; }
    }

}
