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
        public bool Create(Room item)
        {
            try
            {
                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = RoomsQueries.InserNewRoomQueries();

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

                dbCommand.Parameters.Add(startDateParameter);

                DbParameter moderatorIdParameter = dbCommand.CreateParameter();
                moderatorIdParameter.ParameterName = "@userId";
                moderatorIdParameter.Value = item.ModeratorId;
                moderatorIdParameter.DbType = DbType.Decimal;

                dbCommand.Parameters.Add(moderatorIdParameter);

                GenericDataAccess.ExecuteNonQueryCommand(dbCommand);
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        public bool Delete(Room item)
        {
            throw new NotImplementedException();
        }

        public IList<Room> GetAll()
        {
            throw new NotImplementedException();
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
    }
}
