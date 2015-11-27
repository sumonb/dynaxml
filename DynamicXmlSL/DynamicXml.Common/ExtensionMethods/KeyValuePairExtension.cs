using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DynamicXml.Common.ExtensionMethods
{
    public static class KeyValuePairExtension
    {
        private static SqlParameter ToSqlParameter(this KeyValuePair<string, object> pair)
        {
            return new SqlParameter("@" + pair.Key,
                                    pair.Value.GetType().IsEnum ? pair.Value.ToString() : pair.Value);
        }

        //private static SqlParameter[] ToSqlParameterCollection(this IEnumerable<KeyValuePair<string, object>> parameters)
        //{
        //    return parameters.Select(p => new SqlParameter(p.Key, p.Value)).ToArray();
        //}

        //private static SqlParameter[] ToSqlParameterCollection<T>(this IEnumerable<T> data, IEnumerable<KeyValuePair<string, object>> parameters)
        //{
        //    return parameters.Select(p => new SqlParameter(p.Key, p.Value)).ToArray();
        //}


    }
}
