using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityChat.Data.DataHelpers
{
    public class ClassQueries
    {
        public static string SellectAllClassesQuery
        {
            get
            {
                string sql = @"SELECT * FROM [ucdatabase].[UniversityChat].[Classes]";
                return sql;
            }
        }
    }
}
