using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityChat.Data.DataHelpers
{
    public class RoomsQueries
    {
        public static string InserNewRoomQueries
        {
            get
            {
                string sql = @"INSERT INTO [ucdatabase].[UniversityChat].[Rooms]
                        ([RoomId],[RoomName],[RoomDesc],[ClassId],[IsPrivate]
                            ,[IsActive],[ExpirationDate],[StartDate],[LastUsedDate]
                            ,[ModeratorId])
                        VALUES
                        (NEWID(), @roomName, @roomDesc, @classId, 0, 1, @expirationDate,
                        @startDate, @lastUsedDate, NEWID())"; //@userId)";

                return sql;
            }
        }

        public static string DeleteRoomByNameQuery
        {
            get
            {
                string sql = @"DELETE FROM [ucdatabase].[UniversityChat].[Rooms] WHERE RoomName = @roomName";
                return sql;
            }
        }

        public static string SelectAllRoomsQuery
        {
            get
            {
                string sql = @"SELECT * FROM [ucdatabase].[UniversityChat].[Rooms]";
                return sql;
            }
        }

        public static string SelectByRoomName
        {
            get
            {
                string sql = @"SELECT * FROM [ucdatabase].[UniversityChat].[Rooms] WHERE [RoomName] = @roomName";
                return sql;
            }
        }

        public static string SelectByRoomId
        {
            get
            {
                string sql = @"SELECT * FROM [ucdatabase].[UniversityChat].[Rooms] WHERE [RoomId] = @roomId";
                return sql;
            }
        }
    }
}
