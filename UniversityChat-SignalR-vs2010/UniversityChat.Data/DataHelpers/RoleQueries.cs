using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityChat.Data.DataHelpers
{
    public class RoleQueries
    {
        public static string InsertNewRoleQuery()
        {
            string sql = @"INSERT INTO [ucdatabase].[UniversityChat].[Roles] ([RoleId], [RoleName], [RoleDesc])
                            VALUES (@roleId, @roleName, @roleDesc)";

            return sql;
        }

        public static string DeleteRoleQuery()
        {
            string sql = @"DELETE FROM [ucdatabase].[UniversityChat].[Roles] WHERE RoleId = @roleId OR RoleName = @roleName";
            return sql;
        }

        public static string GetAllRolesQuery()
        {
            string sql = @"SELECT RoleId, RoleName, RoleDesc FROM [ucdatabase].[UniversityChat].[Roles]";
            return sql;
        }

        public static string GetRoleByNameQuery()
        {
            string sql = @"SELECT RoleId, RoleName, RoleDesc FROM [ucdatabase].[UniversityChat].[Roles] WHERE RoleName = @roleName";
            return sql;
        }

        public static string GetRoleIdQuery()
        {
            string sql = @"SELECT RoleId FROM [ucdatabase].[UniversityChat].[Roles] WHERE RoleName = @roleName";
            return sql;
        }
    }
}
