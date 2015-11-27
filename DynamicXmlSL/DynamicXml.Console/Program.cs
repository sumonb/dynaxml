using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicXml.Common;
using DynamicXml.Dal;
using DynamicXml.Common.ExtensionMethods;
using Microsoft.CSharp;
using DynamicXml.Bll;

namespace DynamicXml.Console
{
    class Program
    {




        static void Main(string[] args)
        {
          

            var xhelp = new Processor();

            PacketConfiguration packConfig = new PacketConfiguration();

            var retConfig = packConfig.GetConfiguration();

            var rootTable = retConfig[0].Dt;
            int ctr = 1;
            foreach (DataRow row in rootTable.Rows)
            {
                var newDt = row.GenerateTable(rootTable);

                retConfig[0].Dt = newDt;


                xhelp.StartNew(string.Format(@"C:\Temp\dynaxml\{0}.xml", ctr), true);


                xhelp.WriteMultipleDatatable(retConfig);
                xhelp.CloseWriter();
                ctr++;
            }



            //xhelp.StartNew(string.Format(@"E:\Projects\Ref\Tmp\{0}.xml", ctr), true);;

            //xhelp.WriteMultipleDatatable(dd);
            //xhelp.CloseWriter();






            //System.Console.Write(ds.GetXml());
            //System.IO.File.WriteAllText(@"C:\Temp\dynaxml\a.xml", ds.GetXml());
            //

            //DataSet ds = new DataSet("root");
            //// Create a Parent DataTable and add it to the DataSet
            //DataTable table1 = new DataTable("Product");
            //table1.Columns.Add("ProductId", typeof(int));
            //table1.Columns.Add("TID", typeof(int));
            //table1.Columns.Add("Name", typeof(string)).MaxLength = 30;
            //ds.Tables.Add(table1);

            //// Create the child DataTable and add it to the DataSet
            //DataTable table2 = new DataTable("Order");
            //table2.Columns.Add("ProductId", typeof(int));
            //table2.Columns.Add("TmpId", typeof(int));
            //table2.Columns.Add("Customer", typeof(string)).MaxLength = 30;
            //ds.Tables.Add(table2);



            //// Create the child DataTable and add it to the DataSet
            //DataTable table3 = new DataTable("Test");
            //table3.Columns.Add("TmpId", typeof(int));
            //table3.Columns.Add("Customer", typeof(string)).MaxLength = 30;
            //ds.Tables.Add(table3);


            //DataRelation dr = new DataRelation("Order_Product_Relation",
            //  table1.Columns["ProductId"],
            //   table2.Columns["ProductId"]
            //  );
            //dr.Nested = true;
            //ds.Relations.Add(dr);



            //DataRelation drsec = new DataRelation("see",
            //  table2.Columns["TmpId"],
            //   table3.Columns["TmpId"]
            //  );
            //drsec.Nested = true;
            //ds.Relations.Add(drsec);


            //table1.Rows.Add(new object[] { 1, 10, "Germany" });
            //table1.Rows.Add(new object[] { 2, 15, "Japan" });
            //table1.Rows.Add(new object[] { 3, 20, "Italy" });

            //table2.Rows.Add(new object[] { 1, 10, "one" });
            //table2.Rows.Add(new object[] { 1, 15, "two", });
            //table2.Rows.Add(new object[] { 1, 20, "three" });
            ////table2.Rows.Add(new object[] { 3, 3, "Japan", "Smith" });

            //table3.Rows.Add(new object[] { 10, "d" });
            //table3.Rows.Add(new object[] { 15, "p", });
            //table3.Rows.Add(new object[] { 20, "b" });


            //table1.Columns[0].ColumnMapping = MappingType.Attribute;
            //table2.Columns[0].ColumnMapping = MappingType.Hidden;



            //System.Console.WriteLine(ds.GetXml());

            //System.IO.File.WriteAllText(@"C:\Temp\dynaxml\a.xml", ds.GetXml());
            ////


            //System.Console.ReadLine();

            //System.Console.ReadLine();
        }

        public static DataTable CreateMemTable(string tableName, string columnName, List<long> prm)
        {
            DataTable table1 = new DataTable(tableName);
            table1.Columns.Add(columnName, typeof(int));

            foreach (var currentValue in prm)
            {
                DataRow row = table1.NewRow();
                row[columnName] = currentValue;
                table1.Rows.Add(row);
            }

            return table1;
        }

        public static void RemoveTimezoneForDataSet(DataSet ds)
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





//string xmlAsWritten = @"<?xml version=""1.0"" standalone=""yes""?>
//                            <DocumentElement>
//                            <accData>
//                                <Date>2012-03-24T00:00:00+05:30</Date>
//                                <Credit>500</Credit>
//                                <Debit>300</Debit>
//                            </accData>
//                            <accData>
//                                <Date>2012-12-05T00:00:00+05:30</Date>
//                                <Credit>350</Credit>
//                                <Debit>275</Debit>
//                            </accData>
//                            </DocumentElement>";
//string modifiedXml = Regex.Replace(xmlAsWritten,
//                        @"<Date>(?<year>\d{4})-(?<month>\d{2})-(?<date>\d{2}).*?</Date>",    
//                        @"<Date>${date}/${month}/${year}</Date>", 
//                        RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
 