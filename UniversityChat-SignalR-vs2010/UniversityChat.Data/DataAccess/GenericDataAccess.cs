using System;
using System.Data;
using System.Data.Common;

namespace UniversityChat.Data.DataAccess
{
    public static class GenericDataAccess
    {
        static GenericDataAccess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        // executes a command and returns the results as a DataTable object
        public static DataTable ExecuteCommand(DbCommand command)
        {
            DataTable table;

            try
            {
                command.Connection.Open();

                DbDataReader reader = command.ExecuteReader();
                table = new DataTable();
                table.Load(reader);

                reader.Close();
            }
            catch (Exception exp)
            {
                //TODO: LogError
                throw exp;
            }
            finally
            {
                command.Connection.Close();
            }
            return table;
        }

        public static int ExecuteNonQueryCommand(DbCommand command)
        {
            DataTable table;
            int returnedValue = -1;
            try
            {
                command.Connection.Open();

                returnedValue = command.ExecuteNonQuery();                
            }
            catch (Exception exp)
            {
                //TODO: LogError
                throw exp;
            }
            finally
            {
                command.Connection.Close();
            }
            return returnedValue;
        }

        public static string ExecuteStringScalarCommand(DbCommand command)
        {
            DataTable table;
            string returnedValue = string.Empty;

            try
            {
                command.Connection.Open();

                returnedValue = (string)command.ExecuteScalar();
                
            }
            catch (Exception exp)
            {
                //TODO: LogError
                throw exp;
            }
            finally
            {
                command.Connection.Close();
            }
            return returnedValue;
        }

        public static DbCommand CreateCommand(string sqlStringCommand)
        {
           return CreateCommand(sqlStringCommand, null);
        }

        public static DbCommand CreateCommand()
        {
            string dataProviderName = UniversityChatConfiguration.DbProviderName;
            string connectionString = UniversityChatConfiguration.DbConnectionString;

            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProviderName);

            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = connectionString;

            DbCommand comm = conn.CreateCommand();          

            return comm;
        }

        public static DbCommand CreateCommand(string sqlStringCommand, DbParameterCollection dbParameters)
        {
            string dataProviderName = UniversityChatConfiguration.DbProviderName;
            string connectionString = UniversityChatConfiguration.DbConnectionString;

            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProviderName);

            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = connectionString;
            
            DbCommand comm = conn.CreateCommand();
            comm.CommandType = CommandType.Text;

            //Create the dbParameter:
            if(dbParameters != null)
            {
                foreach(DbParameter param in dbParameters)
                {
                    DbParameter dbParameter = comm.CreateParameter();
                    dbParameter.ParameterName = param.ParameterName;
                    dbParameter.Value = param.Value;
                    dbParameter.DbType = param.DbType;

                    comm.Parameters.Add(dbParameter);
                }
            }
            
            //Set the command text:
            comm.CommandText = sqlStringCommand;

            return comm;
        }
    }
}