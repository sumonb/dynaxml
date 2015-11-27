using DynamicXml.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicXml.Common;

namespace DynamicXml.Bll
{
    public class PacketConfiguration
    {

        //public  PacketConfiguration(IDynamicContext dynamicContext)
        //{
        //}
        public List<PacketConfigurationInfo> GetConfiguration()
        {


            IDynamicContext con = new DynamicContext("data source=##;initial catalog=##;persist security info=True;user id=##;password=##;");


            var dto = con.FetchData("root", CommandType.Text, "select * from FRListing", null);




            var dtclient = con.FetchData("listing", CommandType.Text, "select * from FRListingPayment", null);



            List<PacketConfigurationInfo> dd = new List<PacketConfigurationInfo>()
            {
                new PacketConfigurationInfo()
                {
                    Dt = dto,
                    MainElementName = "Listings",
                    ItemElementName = "Listing",
                    RowId = 1,
                    ParentId = 0,
                    PrimaryKey = "FRListingID",
                    RelationKey = "FRListingID",
                    XmlNodeMetaInfo = new List<XmlElementMetaInfo>()
                    {
                        new XmlElementMetaInfo(){AttributeType = true, Name = "FRListingID" }
                       
                    }
                }
                ,
                new PacketConfigurationInfo()
                {
                    Dt = dtclient,
                    MainElementName = "Payments",
                    ItemElementName = "Payment",
                    RowId = 2,
                    ParentId = 1,
                    PrimaryKey = "FRListingPaymentID",
                    RelationKey = "FRListingID",
                    XmlNodeMetaInfo = new List<XmlElementMetaInfo>()
                    {
                        new XmlElementMetaInfo(){AttributeType = true, Name = "FRListingID" },
                        new XmlElementMetaInfo(){AttributeType = true, Name = "OfficeId" }
                       
                    }
                     
                } };

            return dd;
               
        }

    }

    public class PacketConfigurationInfo
    {
        public int RowId { get; set; }
        public DataTable Dt { get; set; }
        public string MainElementName { get; set; }
        public string ItemElementName { get; set; }
        public int ParentId { get; set; }
        public string PrimaryKey { get; set; }
        public string RelationKey { get; set; }
        public List<XmlElementMetaInfo> XmlNodeMetaInfo { get; set; }

    }

}
