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
    public class LogRepository : IRepository<LogMessage>
    {
        public bool Create(LogMessage item)
        {
            try
            {
                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = LogQueries.InserNewLogMessageQuery;

                DbParameter connectionIdParameter = dbCommand.CreateParameter();
                connectionIdParameter.ParameterName = "@connectionId";
                connectionIdParameter.Value = item.ConnectionId;
                connectionIdParameter.DbType = DbType.Guid;

                dbCommand.Parameters.Add(connectionIdParameter);

                DbParameter userIdParameter = dbCommand.CreateParameter();
                userIdParameter.ParameterName = "@userId";
                userIdParameter.Value = item.UserId;
                userIdParameter.DbType = DbType.Guid;

                dbCommand.Parameters.Add(userIdParameter);

                DbParameter logTimeParameter = dbCommand.CreateParameter();
                logTimeParameter.ParameterName = "@logTime";
                logTimeParameter.Value = item.Time;
                logTimeParameter.DbType = DbType.Date;

                dbCommand.Parameters.Add(logTimeParameter);

                DbParameter eventParameter = dbCommand.CreateParameter();
                eventParameter.ParameterName = "@event";
                eventParameter.Value = item.Message;
                eventParameter.DbType = DbType.String;

                dbCommand.Parameters.Add(eventParameter);

                GenericDataAccess.ExecuteNonQueryCommand(dbCommand);
                return true;
            }
            catch (Exception exp)
            {
                //TODO: Log Exception and then handle it.
                return false;
            }
        }

        public bool Delete(LogMessage item)
        {
            throw new NotImplementedException();
        }

        public IList<LogMessage> GetAll()
        {
            try
            {
                List<LogMessage> logMessages = new List<LogMessage>();
                DbCommand dbCommand = GenericDataAccess.CreateCommand();
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = LogQueries.SelectAllQuery;

                DataTable dataTable = GenericDataAccess.ExecuteCommand(dbCommand);

                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        LogMessage logMessage = new LogMessage();

                        try
                        {
                            logMessage.LogNumber = int.Parse(row["LogNumber"].ToString());
                            logMessage.ConnectionId = Guid.Parse(row["ConnectionId"].ToString());
                            logMessage.UserId = Guid.Parse(row["UserId"].ToString());
                            logMessage.Time = DateTime.Parse(row["LogTime"].ToString());
                            logMessage.Message = row["Event"].ToString();
                            logMessages.Add(logMessage);
                        }
                        catch (Exception exp)
                        {
                            continue;
                        }
                    }
                }
                return logMessages;
            }
            catch (Exception exp)
            {
                return new List<LogMessage>(); ;
            }
        }

        public LogMessage GetById(decimal id)
        {
            throw new NotImplementedException();
        }

        public LogMessage GetByCriteria(string criteriaName, LogMessage item)
        {
            throw new NotImplementedException();
        }

        public bool Update(LogMessage item)
        {
            throw new NotImplementedException();
        }
    }
}
