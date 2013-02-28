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
    public class RoomsRepository : IRepository<Room>
    {
        // for now, all rooms use the same class.
        private Guid defaultClassGuid = new ClassRepository().GetDefaultClassGuid();

        public bool Create(Room item)
        {
            try
            {
                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = RoomsQueries.InserNewRoomQueries;

                DbParameter roomNameParameter = dbCommand.CreateParameter();
                roomNameParameter.ParameterName = "@roomName";
                roomNameParameter.Value = item.RoomName;
                roomNameParameter.DbType = DbType.String;

                dbCommand.Parameters.Add(roomNameParameter);

                DbParameter roomDescParameter = dbCommand.CreateParameter();
                roomDescParameter.ParameterName = "@roomDesc";
                roomDescParameter.Value = item.RoomDesc;
                roomDescParameter.DbType = DbType.String;

                dbCommand.Parameters.Add(roomDescParameter);

                DbParameter classIdParameter = dbCommand.CreateParameter();
                classIdParameter.ParameterName = "@classId";
                classIdParameter.Value = defaultClassGuid;
                classIdParameter.DbType = DbType.Guid;

                dbCommand.Parameters.Add(classIdParameter);

                DbParameter expirationDateParameter = dbCommand.CreateParameter();
                expirationDateParameter.ParameterName = "@expirationDate";
                expirationDateParameter.Value = item.ExpirationDate;
                expirationDateParameter.DbType = DbType.DateTime;

                dbCommand.Parameters.Add(expirationDateParameter);

                DbParameter startDateParameter = dbCommand.CreateParameter();
                startDateParameter.ParameterName = "@startDate";
                startDateParameter.Value = item.StartDate;
                startDateParameter.DbType = DbType.DateTime;

                dbCommand.Parameters.Add(startDateParameter);

                DbParameter lastUsedDateParameter = dbCommand.CreateParameter();
                lastUsedDateParameter.ParameterName = "@lastUsedDate";
                lastUsedDateParameter.Value = item.LastUsedDate;
                lastUsedDateParameter.DbType = DbType.DateTime;

                dbCommand.Parameters.Add(lastUsedDateParameter);

                // TODO: since we don't have moderators right now, this is disabled...
                //
                //DbParameter moderatorIdParameter = dbCommand.CreateParameter();
                //moderatorIdParameter.ParameterName = "@userId";
                //moderatorIdParameter.Value = item.ModeratorId;
                //moderatorIdParameter.DbType = DbType.Decimal;

                //dbCommand.Parameters.Add(moderatorIdParameter);

                GenericDataAccess.ExecuteNonQueryCommand(dbCommand);
                return true;
            }
            catch (Exception exp)
            {
                // TODO this should probably not fail silently...
                return false;
            }
        }

        public bool Delete(Room item)
        {
            throw new NotImplementedException();
        }

        public IList<Room> GetAll()
        {
            try
            {
                List<Room> roomsList = new List<Room>();
                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = RoomsQueries.SelectAllRoomsQuery;

                DataTable dataTable = GenericDataAccess.ExecuteCommand(dbCommand);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Room room = new Room();

                        try
                        {
                            room.Id = Guid.Parse(row["RoomId"].ToString());
                            room.RoomName = row["RoomName"].ToString();
                            room.RoomDesc = row["RoomDesc"].ToString();
                            room.ModeratorId = Guid.Parse(row["ModeratorId"].ToString());
                            roomsList.Add(room);
                        }
                        catch(Exception exp)
                        {
                            continue;
                        }
                    }
                }
                return roomsList;
            }
            catch (Exception exp)
            {
                return new List<Room>(); ;
            }
        }

        public Guid GetGuidByName(string roomName)
        {
            try
            {
                Guid roomGuid = Guid.Empty;

                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = RoomsQueries.SelectByRoomName;

                DbParameter roomNameParameter = dbCommand.CreateParameter();
                roomNameParameter.ParameterName = "@roomName";
                roomNameParameter.Value = roomName;
                roomNameParameter.DbType = DbType.String;
                dbCommand.Parameters.Add(roomNameParameter);

                DataTable dataTable = GenericDataAccess.ExecuteCommand(dbCommand);

                if (dataTable != null)
                {
                    DataRow room = dataTable.Rows[0];
                    roomGuid = Guid.Parse(room["RoomId"].ToString());
                }
                return roomGuid;
            }
            catch (Exception exp)
            {
                return Guid.Empty;
            }
        }

        public Room GetById(decimal id)
        {
            throw new NotImplementedException();
        }

        public Room GetByCriteria(string criteriaName, Room item)
        {
            throw new NotImplementedException();
        }

        public bool Update(Room item)
        {
            throw new NotImplementedException();
        }

        public Room GetById(Guid roomId)
        {
            try
            {
                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = RoomsQueries.SelectByRoomId;

                DbParameter roomNameParameter = dbCommand.CreateParameter();
                roomNameParameter.ParameterName = "@roomId";
                roomNameParameter.Value = roomId;
                roomNameParameter.DbType = DbType.Guid;
                dbCommand.Parameters.Add(roomNameParameter);

                DataTable dataTable = GenericDataAccess.ExecuteCommand(dbCommand);

                Room room = null;
                if (dataTable != null)
                {
                    room = new Room(dataTable.Rows[0]);
                }
                return room;
            }
            catch (Exception exp)
            {
                return null;
            }
        }
    }
}
