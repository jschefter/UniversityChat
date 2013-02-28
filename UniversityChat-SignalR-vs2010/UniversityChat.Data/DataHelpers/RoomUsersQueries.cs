using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityChat.Data.DataHelpers
{
    public class RoomUsersQueries
    {
        public static string InserNewRoomUsersQueries
        {
            get
            {
                string sql = @"INSERT INTO [ucdatabase].[UniversityChat].[RoomUsers]
                        ([RoomId],[UserId])
                        VALUES
                        (@roomId, @userId)";

                return sql;
            }

        }

        public static string DeleteRoomByRoomIdAndUserId
        {
            get
            {
                string sql = @"DELETE FROM [ucdatabase].[UniversityChat].[RoomUsers] WHERE RoomId = @roomId AND UserId = @userId";
                return sql;
            }
        }

        public static string SellectAllRoomUsersQuery
        {
            get
            {
                string sql = @"SELECT * FROM [ucdatabase].[UniversityChat].[RoomUsers]";
                return sql;
            }
        }

        public static string SelectByRoomId
        {
            get
            {
                string sql = @"SELECT * FROM [ucdatabase].[UniversityChat].[RoomUsers] WHERE RoomId = @roomId";
                return sql;
            }
        }

        public static string SelectByUserId
        {
            get
            {
                string sql = @"SELECT * FROM [ucdatabase].[UniversityChat].[RoomUsers] WHERE UserId = @userId";
                return sql;
            }
        }
    }
}
