using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityChat.Data.DataHelpers
{
    public class LogQueries
    {
        public static string InserNewLogMessageQuery
        {
            get
            {
                string sql = @"INSERT INTO [ucdatabase].[UniversityChat].[Log]
                            ([ConnectionId], [UserId],[LogTime], [Event])
                            VALUES (@connectionId, @userId, @logTime, @event)";

                return sql;
            }
        }

        public static string SelectAllQuery
        {
            get
            {
                string sql = @"SELECT * FROM [ucdatabase].[UniversityChat].[Log]";
                return sql;
            }
        }
    }
}
