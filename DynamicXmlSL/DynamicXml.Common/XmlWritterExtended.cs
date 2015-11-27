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


        public void WriteSmartElement(string prmHeaderElementName, DataRow prmCurrentRow, DataTable dt, List<string> prmAttributeCollection )
        {
            var itemElementPresent = !string.IsNullOrWhiteSpace(prmHeaderElementName);
            if (itemElementPresent)
            {
                base.WriteStartElement(prmHeaderElementName);
            }

            //now print attribute
            foreach (var currentAttribute in prmAttributeCollection)
            {
                base.WriteAttributeString(currentAttribute, prmCurrentRow[currentAttribute].ToString());
            }

            
            foreach (DataColumn col in dt.Columns)
            {
                //don't write element if were part of attribute
                var ret = prmAttributeCollection.Count(i => i == col.ColumnName);
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
}
