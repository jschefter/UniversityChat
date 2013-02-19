using System.Configuration;

public static class UniversityChatConfiguration
{
    private static string dbConnectionString;
    private static string dbProviderName;

    static UniversityChatConfiguration()
    {
        dbConnectionString = @"";
        dbProviderName = @"";
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
