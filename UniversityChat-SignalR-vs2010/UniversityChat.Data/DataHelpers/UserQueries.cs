using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityChat.Data.DataHelpers
{
    public class UserQueries
    {
        public static string InserNewUserQuery()
        {
            string sql = @"INSERT INTO [ucdatabase].[UniversityChat].[Users]
                            ([UserId], [FName],[LName], [NickName],[Email],[UserRoleId])
                            VALUES (NEWID(), @fName, @lName, @nickName, @email, @roleId)";

            return sql;
        }

        public static string DeleteUserByIdQuery()
        {
            string sql = @"DELETE FROM [ucdatabase].[UniversityChat].[Roles] WHERE UserId = @id";
            return sql;
        }

        public static string DeleteUserByNickNameQuery()
        {
            string sql = @"DELETE FROM [ucdatabase].[UniversityChat].[Users] WHERE NickName = @nickName";
            return sql;
        }
    }
}
