using API.Login.Utils.Email;
using Microsoft.Extensions.Configuration;

namespace API.Login.Utils;
public interface IAppConfiguration
{
    string GetSqlLiteConnectionString();
    EmailConfiguration GetEmailConfiguration();
    string ReturnRegisterConfirmationLink();
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
        var emailConfiguration = new EmailConfiguration(this,_config);
        return emailConfiguration;
    }

    public string GetTokenEncodeKey()
    {
        var key = _config.GetSection("tokenKey").Value ?? String.Empty;
        return key;
    }

    public string ReturnRegisterConfirmationLink()
    {
        return _config.GetSection("ClientURLs:RegisterConfirmationLink").Value ?? String.Empty;
    }
}
