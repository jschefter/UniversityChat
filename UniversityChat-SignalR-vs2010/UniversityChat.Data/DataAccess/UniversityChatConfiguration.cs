using System.Configuration;

namespace UniversityChat.Data.DataAccess
{
    public static class UniversityChatConfiguration
    {
        private static string dbConnectionString;
        private static string dbProviderName;

        static UniversityChatConfiguration()
        {
            dbConnectionString = @"Server=tcp:yrinp8w9up.database.windows.net,1433;Database=ucdatabase;
                                User ID=ucadmin@yrinp8w9up;Password=css490UniversityChat;
                                Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

            dbProviderName = @"System.Data.SqlClient";
        }

        public static string DbConnectionString
        {
            get
            {
                return dbConnectionString;
            }
        }

        public static string DbProviderName
        {
            get
            {
                return dbProviderName;
            }
        }
    }
}