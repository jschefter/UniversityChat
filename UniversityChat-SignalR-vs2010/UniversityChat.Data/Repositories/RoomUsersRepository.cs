using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityChat.Model;
using System.Data.Common;
using UniversityChat.Data.DataAccess;
using System.Data;
using UniversityChat.Data.DataHelpers;

namespace UniversityChat.Data.Repositories
{
    public class RoomUsersRepository : IRepository<RoomUser>
    {
        public bool Create(RoomUser item)
        {
            try
            {
                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = RoomUsersQueries.InserNewRoomUsersQueries;

                DbParameter roomIdParameter = dbCommand.CreateParameter();
                roomIdParameter.ParameterName = "@roomId";
                roomIdParameter.Value = item.RoomId;
                roomIdParameter.DbType = DbType.Guid;
                dbCommand.Parameters.Add(roomIdParameter);

                DbParameter userIdParameter = dbCommand.CreateParameter();
                userIdParameter.ParameterName = "@userId";
                userIdParameter.Value = item.UserId;
                userIdParameter.DbType = DbType.Guid;
                dbCommand.Parameters.Add(userIdParameter);

                GenericDataAccess.ExecuteNonQueryCommand(dbCommand);
                return true;
            }
            catch (Exception exp)
            {
                // TODO this should probably not fail silently...
                return false;
            }
        }

        public bool Delete(RoomUser item)
        {
            try
            {
                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = RoomUsersQueries.DeleteRoomByRoomIdAndUserId;

                DbParameter roomIdParameter = dbCommand.CreateParameter();
                roomIdParameter.ParameterName = "@roomId";
                roomIdParameter.DbType = DbType.Guid;
                roomIdParameter.Value = item.RoomId;
                dbCommand.Parameters.Add(roomIdParameter);

                DbParameter userIdParameter = dbCommand.CreateParameter();
                userIdParameter.ParameterName = "@userId";
                userIdParameter.DbType = DbType.Guid;
                userIdParameter.Value = item.UserId;
                dbCommand.Parameters.Add(userIdParameter);
                
                int linesAffected = GenericDataAccess.ExecuteNonQueryCommand(dbCommand);

                return (linesAffected == 0) ? false : true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        public IList<RoomUser> GetAll()
        {
            try
            {
                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = RoomUsersQueries.SellectAllRoomUsersQuery;

                DataTable dataTable = GenericDataAccess.ExecuteCommand(dbCommand);

                List<RoomUser> roomUsersList = new List<RoomUser>();
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        try
                        {
                            roomUsersList.Add(new RoomUser(row));
                        }
                        catch(Exception exp)
                        {
                            continue;
                        }
                    }
                }
                return roomUsersList;
            }
            catch (Exception exp)
            {
                return null;
            }
        }


        public ICollection<RoomUser> GetByRoomId(Guid roomId)
        {
            try
            {
                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = RoomUsersQueries.SelectByRoomId;

                DbParameter roomIdParameter = dbCommand.CreateParameter();
                roomIdParameter.ParameterName = "@roomId";
                roomIdParameter.Value = roomId;
                roomIdParameter.DbType = DbType.Guid;
                dbCommand.Parameters.Add(roomIdParameter);

                DataTable dataTable = GenericDataAccess.ExecuteCommand(dbCommand);

                List<RoomUser> roomUsersList = new List<RoomUser>();
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        try
                        {
                            roomUsersList.Add(new RoomUser(row));
                        }
                        catch (Exception exp)
                        {
                            continue;
                        }
                    }
                }
                return roomUsersList;
            }
            catch (Exception exp)
            {
                return new List<RoomUser>();
            }
        }

        public ICollection<RoomUser> GetByUserId(Guid userId)
        {
            try
            {
                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = RoomUsersQueries.SelectByUserId;

                DbParameter roomIdParameter = dbCommand.CreateParameter();
                roomIdParameter.ParameterName = "@userId";
                roomIdParameter.Value = userId;
                roomIdParameter.DbType = DbType.Guid;
                dbCommand.Parameters.Add(roomIdParameter);

                DataTable dataTable = GenericDataAccess.ExecuteCommand(dbCommand);

                List<RoomUser> roomUsersList = new List<RoomUser>();
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        try
                        {
                            roomUsersList.Add(new RoomUser(row));
                        }
                        catch (Exception exp)
                        {
                            continue;
                        }
                    }
                }
                return roomUsersList;
            }
            catch (Exception exp)
            {
                return new List<RoomUser>();
            }
        }

        public RoomUser GetById(decimal id)
        {
            throw new NotImplementedException();
        }

        public RoomUser GetByCriteria(string criteriaName, RoomUser item)
        {
            throw new NotImplementedException();
        }

        public bool Update(RoomUser item)
        {
            throw new NotImplementedException();
        }
    }
}
