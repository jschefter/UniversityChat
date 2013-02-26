using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityChat.Data.DataHelpers
{
    public class RoomsQueries
    {
        public static string InserNewRoomQueries()
        {
            string sql = @"INSERT INTO [ucdatabase].[UniversityChat].[Rooms]
                        ([RoomId],[RoomName],[RoomDesc],[ClassId],[IsPrivate]
                            ,[IsActive],[ExpirationDate],[StartDate],[LastUsedDate]
                            ,[ModeratorId])
                        VALUES
                        (NEWID(), @roomName, @roomDesc, NEWID(), 0, 1, @expirationDate,
                        @startDate, @lastUsedDate,@userId)";

            return sql;

        }
    }
}
