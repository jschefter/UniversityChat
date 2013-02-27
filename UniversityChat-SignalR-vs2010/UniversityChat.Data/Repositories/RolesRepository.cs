using System;
using System.Collections.Generic;
using UniversityChat.Model;
using System.Data.Common;
using UniversityChat.Data.DataAccess;
using System.Data;
using UniversityChat.Data.DataHelpers;

namespace UniversityChat.Data.Repositories
{
    public class RolesRepository: IRepository<Role>
    {
        public bool Create(Role item)
        {
            try
            {
                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = RoleQueries.InsertNewRoleQuery();

                DbParameter roleIdParameter = dbCommand.CreateParameter();
                roleIdParameter.ParameterName = "@roleId";
                roleIdParameter.Value = item.Id;
                roleIdParameter.DbType = DbType.Int16;

                dbCommand.Parameters.Add(roleIdParameter);

                DbParameter roleNameParameter = dbCommand.CreateParameter();
                roleNameParameter.ParameterName = "@roleName";
                roleNameParameter.Value = item.RoleName;
                roleNameParameter.DbType = DbType.String;

                dbCommand.Parameters.Add(roleNameParameter);

                DbParameter roleDescParameter = dbCommand.CreateParameter();
                roleDescParameter.ParameterName = "@roleDesc";
                roleDescParameter.Value = item.ReleDesc;
                roleDescParameter.DbType = DbType.String;

                dbCommand.Parameters.Add(roleDescParameter);

                GenericDataAccess.ExecuteNonQueryCommand(dbCommand);
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        public bool Delete(Role item)
        {
            try
            {
                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = RoleQueries.DeleteRoleQuery();

                DbParameter roleIdParameter = dbCommand.CreateParameter();
                roleIdParameter.ParameterName = "@roleId";
                roleIdParameter.Value = item.Id;
                roleIdParameter.DbType = DbType.Int16;

                dbCommand.Parameters.Add(roleIdParameter);

                DbParameter roleNameParameter = dbCommand.CreateParameter();
                roleNameParameter.ParameterName = "@roleName";
                roleNameParameter.Value = item.RoleName;
                roleNameParameter.DbType = DbType.String;

                dbCommand.Parameters.Add(roleNameParameter);

                GenericDataAccess.ExecuteNonQueryCommand(dbCommand);
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        public IList<Role> GetAll()
        {
            try
            {
                var rolesList = new List<Role>();

                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = RoleQueries.GetAllRolesQuery();

                DataTable dataTable = GenericDataAccess.ExecuteCommand(dbCommand);

                foreach (DataRow row in dataTable.Rows)
                {
                    try
                    {
                        Role role = new Role();
                        if (row["RoleId"] != null)
                        {
                            role.Id = Int16.Parse(row["RoleId"].ToString());
                        }
                        if (row["RoleName"] != null)
                        {
                            role.RoleName = row["RoleName"].ToString();
                        }
                        if (row["RoleDesc"] != null)
                        {
                            role.ReleDesc = row["RoleDesc"].ToString();
                        }
                        rolesList.Add(role);
                    }
                    catch
                    {
                        continue;
                    }

                }
                return rolesList;
            }
            catch (Exception exp)
            {
                return null;
            }
        }       

        public Role GetRoleByName(string roleName)
        {
            try
            {
                var role = new Role();

                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = RoleQueries.GetRoleByNameQuery();

                DbParameter roleNameParameter = dbCommand.CreateParameter();
                roleNameParameter.ParameterName = "@roleName";
                roleNameParameter.Value = roleName;
                roleNameParameter.DbType = DbType.String;

                dbCommand.Parameters.Add(roleNameParameter);

                DataTable dataTable = GenericDataAccess.ExecuteCommand(dbCommand);

                foreach (DataRow row in dataTable.Rows)
                {
                    try
                    {                        
                        if (row["RoleId"] != null)
                        {
                            role.Id = Int16.Parse(row["RoleId"].ToString());
                        }
                        if (row["RoleName"] != null)
                        {
                            role.RoleName = row["RoleName"].ToString();
                        }
                        if (row["RoleDesc"] != null)
                        {
                            role.ReleDesc = row["RoleDesc"].ToString();
                        }
                        break;
                    }
                    catch
                    {
                        continue;
                    }

                }
                return role;
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public Role GetById(decimal id)
        {
            return null;
        }

        public Role GetByCriteria(string criteriaName, Role item)
        {
            throw new NotImplementedException();
        }

        public bool Update(Role item)
        {
            throw new NotImplementedException();
        }
    }
}
