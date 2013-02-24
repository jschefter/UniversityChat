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
        public static DataTable ExecuteSelectCommand(DbCommand command)
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

        public static DbCommand CreateCommand()
        {
            string dataProviderName = UniversityChatConfiguration.DbProviderName;
            string connectionString = UniversityChatConfiguration.DbConnectionString;

            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProviderName);

            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = connectionString;

            DbCommand comm = conn.CreateCommand();
            comm.CommandType = CommandType.Text;
            comm.CommandText = @"SELECT * FROM Test";

            return comm;
        }
    }
}