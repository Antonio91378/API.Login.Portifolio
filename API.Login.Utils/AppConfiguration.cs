using Microsoft.Extensions.Configuration;

namespace API.Login.Utils;

public static class AppConfiguration
{
    public static IConfiguration _config;
    public static void Initialize(IConfiguration Configuration)
    {
        _config = Configuration;
    }
    public static string GetSqlLiteConnectionString()
    {
        var cn = _config.GetSection("ConnectionStrings:SqlLite").Value ?? String.Empty;
        return cn;
    }

    public static EmailConfiguration GetEmailConfiguration()
    {

        string from = _config.GetSection("EmailConfiguration:From").Value ?? String.Empty;
        string smtpServer = _config.GetSection("EmailConfiguration:SmtpServer").Value ?? String.Empty;
        string port = _config.GetSection("EmailConfiguration:Port").Value ?? String.Empty;
        string userName = _config.GetSection("EmailConfiguration:UserName").Value ?? String.Empty;
        string passWord = _config.GetSection("EmailConfiguration:Password").Value ?? String.Empty;
        string displayName = _config.GetSection("EmailConfiguration:DisplayName").Value ?? String.Empty;

        var emailConfiguration = new EmailConfiguration(from, smtpServer, port, userName, passWord, displayName);
        return emailConfiguration;
    }
}
