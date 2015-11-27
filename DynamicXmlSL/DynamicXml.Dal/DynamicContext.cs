using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicXml.Common.ExtensionMethods;

namespace DynamicXml.Dal
{
    public interface IDynamicContext
    {
        DataTable FetchData(string prmDataTableName, 
                            System.Data.CommandType prmCommandType, 
                            string prmCommandText, 
                            IEnumerable<KeyValuePair<string, object>> prmParameters);
    }

    public class DynamicContext : ConnectionFactory, IDynamicContext
    {
        private readonly string _connectionString;

        public DynamicContext(string connectionString): base(connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable FetchData(string prmDataTableName, System.Data.CommandType prmCommandType, string prmCommandText, IEnumerable<KeyValuePair<string, object>> prmParameters)
        {
            var retTable = new DataTable(prmDataTableName);

            using (var con = base.GetConnection())
            {
                using (var command = con.CreateCommand())
                {

                    command.CommandText = prmCommandText;
                    command.CommandType = prmCommandType;
                    command.Parameters.Clear();
                    command.Parameters.AddRange(prmParameters.ToSqlParameterCollection());

                    //fill dataset
                    using (var retAdapter = new SqlDataAdapter(command))
                    {
                        retAdapter.Fill(retTable);
                    }

                }
            }
    
            return retTable;
        }
    }
}
