﻿using System;
using System.Collections.Generic;
using UniversityChat.Model;
using System.Data.Common;
using UniversityChat.Data.DataAccess;
using System.Data;
using UniversityChat.Data.DataHelpers;

namespace UniversityChat.Data.Repositories
{
    public class UsersRepository: IRepository<User>
    {
        public bool Create(User item)
        {
            try
            {
                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = UserQueries.InserNewUserQuery;

                DbParameter fNameParameter = dbCommand.CreateParameter();
                fNameParameter.ParameterName = "@fName";
                fNameParameter.Value = item.FName;
                fNameParameter.DbType = DbType.String;

                dbCommand.Parameters.Add(fNameParameter);

                DbParameter lNameParameter = dbCommand.CreateParameter();
                lNameParameter.ParameterName = "@lName";
                lNameParameter.Value = item.LName;
                lNameParameter.DbType = DbType.String;

                dbCommand.Parameters.Add(lNameParameter);

                DbParameter nickNameParameter = dbCommand.CreateParameter();
                nickNameParameter.ParameterName = "@nickName";
                nickNameParameter.Value = item.NickName;
                nickNameParameter.DbType = DbType.String;

                dbCommand.Parameters.Add(nickNameParameter);

                DbParameter emailParameter = dbCommand.CreateParameter();
                emailParameter.ParameterName = "@email";
                emailParameter.Value = item.EmailAddress;
                emailParameter.DbType = DbType.String;

                dbCommand.Parameters.Add(emailParameter);

                DbParameter passwordParameter = dbCommand.CreateParameter();
                passwordParameter.ParameterName = "@password";
                passwordParameter.Value = item.Password;
                passwordParameter.DbType = DbType.String;

                dbCommand.Parameters.Add(passwordParameter);

                DbParameter roleIdParameter = dbCommand.CreateParameter();
                roleIdParameter.ParameterName = "@roleId";
                roleIdParameter.Value = item.RoleId;
                roleIdParameter.DbType = DbType.Int16;

                dbCommand.Parameters.Add(roleIdParameter);

                GenericDataAccess.ExecuteNonQueryCommand(dbCommand);
                return true;
            }
            catch (Exception exp)
            {
                //TODO: Log Exception and then handle it.
                return false;
            }
        }

        public bool Delete(User item)
        {
            try
            {
                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = UserQueries.DeleteUserQuery;
                
                DbParameter nickNameParameter = dbCommand.CreateParameter();
                nickNameParameter.ParameterName = "@nickName";
                nickNameParameter.Value = item.NickName;
                nickNameParameter.DbType = DbType.String;

                dbCommand.Parameters.Add(nickNameParameter);

                DbParameter emailParameter = dbCommand.CreateParameter();
                emailParameter.ParameterName = "@email";
                emailParameter.Value = item.EmailAddress;
                emailParameter.DbType = DbType.String;

                dbCommand.Parameters.Add(emailParameter);      

                GenericDataAccess.ExecuteNonQueryCommand(dbCommand);
                return true;
            }
            catch (Exception exp)
            {
                //TODO: Log Exception and then handle it.
                return false;
            }
        }

        public IList<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(decimal id)
        {
            throw new NotImplementedException();
        }

        public bool Update(User item)
        {
            throw new NotImplementedException();
        }

        public User GetByCriteria(string criteriaName, User item)
        {
            throw new NotImplementedException();
        }

        public User GetByName(string username)
        {
            try
            {
                User user = null;

                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = UserQueries.SelectByNickName;

                DbParameter nickNameParameter = dbCommand.CreateParameter();
                nickNameParameter.ParameterName = "@nickName";
                nickNameParameter.Value = username;
                nickNameParameter.DbType = DbType.String;
                dbCommand.Parameters.Add(nickNameParameter);

                DataTable dataTable = GenericDataAccess.ExecuteCommand(dbCommand);

                if (dataTable != null)
                {
                    DataRow userData = dataTable.Rows[0];
                    user = new User(userData);
                }
                return user;
            }
            catch (Exception exp)
            {
                return null;
            }
        }

        public User GetByUserId(Guid userId)
        {
            try
            {
                User user = null;

                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = UserQueries.SelectByUserId;

                DbParameter userIdParameter = dbCommand.CreateParameter();
                userIdParameter.ParameterName = "@id";
                userIdParameter.Value = userId;
                userIdParameter.DbType = DbType.Guid;
                dbCommand.Parameters.Add(userIdParameter);

                DataTable dataTable = GenericDataAccess.ExecuteCommand(dbCommand);

                if (dataTable != null)
                {
                    DataRow userData = dataTable.Rows[0];
                    user = new User(userData);
                }
                return user;
            }
            catch (Exception exp)
            {
                return null;
            }
        }
    }
}
