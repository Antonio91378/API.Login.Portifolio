using Microsoft.Extensions.Configuration;

namespace API.Login.Utils;

public static class AppConfiguration
{
    private static IConfiguration config;
    public static void Initialize(IConfiguration Configuration)
    {
        config = Configuration;
    }
    public static string GetSqlLiteConnectionString()
    {
        var cn = config.GetSection("ConnectionStrings:SqlLite").Value;
        return cn;
    }

    public static string GetTokenEncodeKey()
    {
        var key = config.GetSection("Secrets:tokenEncodeKey").Value;
        return key;
    }


}
