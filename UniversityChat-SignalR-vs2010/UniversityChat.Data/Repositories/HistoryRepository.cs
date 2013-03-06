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
    public class HistoryRepository : IRepository<History>
    {
        public bool Create(History item)
        {
            try
            {
                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = HistoryLogQueries.InserNewHistoryLogQuery;

                DbParameter userIdParameter = dbCommand.CreateParameter();
                userIdParameter.ParameterName = "@UserId";
                userIdParameter.Value = item.UserId;
                userIdParameter.DbType = DbType.Guid;

                dbCommand.Parameters.Add(userIdParameter);

                DbParameter connectionIdParameter = dbCommand.CreateParameter();
                connectionIdParameter.ParameterName = "@connectionId";
                connectionIdParameter.Value = item.ConnectionId;
                connectionIdParameter.DbType = DbType.Guid;

                dbCommand.Parameters.Add(connectionIdParameter);

                DbParameter roomIdParameter = dbCommand.CreateParameter();
                roomIdParameter.ParameterName = "@roomId";
                roomIdParameter.Value = item.RoomId;
                roomIdParameter.DbType = DbType.Guid;

                dbCommand.Parameters.Add(roomIdParameter);

                DbParameter textParameter = dbCommand.CreateParameter();
                textParameter.ParameterName = "@text";
                textParameter.Value = item.Text;
                textParameter.DbType = DbType.String;

                dbCommand.Parameters.Add(textParameter);

                DbParameter logDateTimeStampParameter = dbCommand.CreateParameter();
                logDateTimeStampParameter.ParameterName = "@logDateTimeStamp";
                logDateTimeStampParameter.Value = item.LogDateTimeStamp;
                logDateTimeStampParameter.DbType = DbType.DateTime;

                dbCommand.Parameters.Add(logDateTimeStampParameter);               

                GenericDataAccess.ExecuteNonQueryCommand(dbCommand);
                return true;
            }
            catch (Exception exp)
            {
                //TODO: Handle Exceptions
                return false;
            }
        }

        public bool Delete(History item)
        {
            throw new NotImplementedException();
        }

        public IList<History> GetHistoryLogByRoomId(Guid roomId)
        {
            try
            {
                var historyList = new List<History>();

                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = HistoryLogQueries.GetAllHistoryLogByRoomIdQuery;

                DbParameter roomIdParameter = dbCommand.CreateParameter();
                roomIdParameter.ParameterName = "@roomId";
                roomIdParameter.Value = roomId;
                roomIdParameter.DbType = DbType.Guid;

                dbCommand.Parameters.Add(roomIdParameter);

                DataTable dataTable = GenericDataAccess.ExecuteCommand(dbCommand);                 

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var historyLog = new History();

                        try
                        {
                            historyLog.Id = Guid.Parse(row["Id"].ToString());
                            historyLog.UserId = Guid.Parse(row["UserId"].ToString());
                            historyLog.ConnectionId = Guid.Parse(row["ConnectionId"].ToString());
                            historyLog.RoomId = Guid.Parse(row["RoomId"].ToString());
                            historyLog.Text = row["Text"].ToString();
                            historyLog.LogDateTimeStamp = DateTime.Parse(row["LogDateTimeStamp"].ToString());
                            historyList.Add(historyLog);
                        }
                        catch (Exception exp)
                        {
                            continue;
                        }
                    }
                }
                return historyList;                
            }
            catch (Exception exp)
            {
                //TODO: Handle Exceptions
                return null;
            }
        }

        public IList<History> GetAll()
        {           
            try
            {
                var historyList = new List<History>();

                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = HistoryLogQueries.GetAllHistoryLogQuery;            

                DataTable dataTable = GenericDataAccess.ExecuteCommand(dbCommand);                 

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var historyLog = new History();

                        try
                        {
                            historyLog.Id = Guid.Parse(row["Id"].ToString());
                            historyLog.UserId = Guid.Parse(row["UserId"].ToString());
                            historyLog.ConnectionId = Guid.Parse(row["ConnectionId"].ToString());
                            historyLog.RoomId = Guid.Parse(row["RoomId"].ToString());
                            historyLog.Text = row["Text"].ToString();
                            historyLog.LogDateTimeStamp = DateTime.Parse(row["LogDateTimeStamp"].ToString());
                            historyList.Add(historyLog);
                        }
                        catch (Exception exp)
                        {
                            continue;
                        }
                    }
                }
                return historyList;                
            }
            catch (Exception exp)
            {
                //TODO: Handle Exceptions
                return null;
            }
        }

        public History GetById(decimal id)
        {
            throw new NotImplementedException();
        }

        public History GetByCriteria(string criteriaName, History item)
        {
            throw new NotImplementedException();
        }

        public bool Update(History item)
        {
            throw new NotImplementedException();
        }        
    }
}
