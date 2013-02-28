using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityChat.Data.DataHelpers
{
    public class RoleQueries
    {
        public static string InsertNewRoleQuery
        {
            get
            {
                string sql = @"INSERT INTO [ucdatabase].[UniversityChat].[Roles] ([RoleId], [RoleName], [RoleDesc])
                            VALUES (@roleId, @roleName, @roleDesc)";

                return sql;
            }
        }

        public static string DeleteRoleQuery
        {
            get
            {
                string sql = @"DELETE FROM [ucdatabase].[UniversityChat].[Roles] WHERE RoleId = @roleId OR RoleName = @roleName";
                return sql;
            }
        }

        public static string GetAllRolesQuery
        {
            get
            {
                string sql = @"SELECT RoleId, RoleName, RoleDesc FROM [ucdatabase].[UniversityChat].[Roles]";
                return sql;
            }
        }

        public static string GetRoleByNameQuery
        {
            get
            {
                string sql = @"SELECT RoleId, RoleName, RoleDesc FROM [ucdatabase].[UniversityChat].[Roles] WHERE RoleName = @roleName";
                return sql;
            }
        }

        public static string GetRoleIdQuery
        {
            get
            {
                string sql = @"SELECT RoleId FROM [ucdatabase].[UniversityChat].[Roles] WHERE RoleName = @roleName";
                return sql;
            }
        }
    }
}
