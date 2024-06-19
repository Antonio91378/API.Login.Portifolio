using Microsoft.Extensions.Configuration;

namespace API.Login.Utils;
public interface IAppConfiguration
{
    string GetSqlLiteConnectionString();
    EmailConfiguration GetEmailConfiguration();
    string GetTokenEncodeKey();
}
public class AppConfiguration : IAppConfiguration
{
    private readonly IConfiguration _config;
    public AppConfiguration(IConfiguration configuration)
    {
        _config = configuration;
    }
    
    public string GetSqlLiteConnectionString()
    {
        var cn = _config.GetSection("ConnectionStrings:SqlLite").Value ?? String.Empty;
        return cn;
    }

    public EmailConfiguration GetEmailConfiguration()
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

    public string GetTokenEncodeKey()
    {
        var key = _config.GetSection("Secrets:tokenEncodeKey").Value ?? String.Empty;
        return key;
    }
}
