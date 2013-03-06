using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityChat.Data.DataHelpers
{
    public class HistoryLogQueries
    {
        public static string InserNewHistoryLogQuery
        {
            get
            {
                string sql = @"INSERT INTO [ucdatabase].[UniversityChat].[History]
                            ([Id], [UserId], [ConnectionId],[RoomId], [Text],[LogDateTimeStamp])
                            VALUES (NEWID(), @userId, @connectionId, @roomId, @text, @logDateTimeStamp)";

                return sql;
            }
        }

        public static string GetAllHistoryLogByRoomIdQuery
        {
            get
            {
                string sql = @"SELECT * FROM [ucdatabase].[UniversityChat].[History] WHERE RoomId = @roomId";
                return sql;
            }
        }

        public static string GetAllHistoryLogQuery
        {
            get
            {
                string sql = @"SELECT * FROM [ucdatabase].[UniversityChat].[History]";
                return sql;
            }
        }
    }
}
