using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicXml.Common.ExtensionMethods
{
    public static class EnumerableExtension
    {
        //private static SqlParameter[] ToSqlParameterCollection(this IEnumerable<KeyValuePair<string, object>> prm)
        //{
        //    return prm.Select(p => new SqlParameter(p.Key, p.Value)).ToArray();
        //}

        public static SqlParameter[] ToSqlParameterCollection(this IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return parameters == null ? new SqlParameter[0] : parameters.Select(p => new SqlParameter(p.Key, p.Value)).ToArray();
        }

        // Count<TSource>(this IEnumerable<TSource> source)


    }
}
