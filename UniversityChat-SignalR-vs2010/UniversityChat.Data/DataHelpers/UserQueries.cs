using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityChat.Data.DataHelpers
{
    public class UserQueries
    {
        public static string InserNewUserQuery
        {
            get
            {
                string sql = @"INSERT INTO [ucdatabase].[UniversityChat].[Users]
                            ([UserId], [FName],[LName], [NickName],[Email],[UserRoleId])
                            VALUES (NEWID(), @fName, @lName, @nickName, @email, @roleId)";

                return sql;
            }
        }

        public static string DeleteUserQuery
        {
            get
            {
                string sql = @"DELETE FROM [ucdatabase].[UniversityChat].[Users] WHERE NickName = @nickName OR Email = @email";
                return sql;
            }
        }

        public static string DeleteUserByIdQuery
        {
            get
            {
                string sql = @"DELETE FROM [ucdatabase].[UniversityChat].[Users] WHERE UserId = @id";
                return sql;
            }
        }

        public static string DeleteUserByNickNameQuery
        {
            get
            {
                string sql = @"DELETE FROM [ucdatabase].[UniversityChat].[Users] WHERE NickName = @nickName";
                return sql;
            }
        }

        public static string SelectByNickName
        {
            get
            {
                string sql = @"SELECT * FROM [ucdatabase].[UniversityChat].[Users] WHERE NickName = @nickName";
                return sql;
            }
        }

        public static string SelectByUserId
        {
            get
            {
                string sql = @"SELECT * FROM [ucdatabase].[UniversityChat].[Users] WHERE UserId = @id";
                return sql;
            }
        }
    }
}
