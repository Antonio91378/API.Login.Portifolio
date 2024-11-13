using Microsoft.Extensions.Configuration;

namespace API.Login.Utils.Email;

public class EmailConfiguration
{
    public readonly IAppConfiguration _appConfiguration;
    public EmailConfiguration(IAppConfiguration appConfiguration, IConfiguration _config)
    {
        string from = _config.GetSection("EmailConfiguration:From").Value ?? string.Empty;
        string smtpServer = _config.GetSection("EmailConfiguration:SmtpServer").Value ?? string.Empty;
        string port = _config.GetSection("EmailConfiguration:Port").Value ?? string.Empty;
        string userName = _config.GetSection("EmailConfiguration:UserName").Value ?? string.Empty;
        string passWord = _config.GetSection("EmailConfiguration:Password").Value ?? string.Empty;
        string displayName = _config.GetSection("EmailConfiguration:DisplayName").Value ?? string.Empty;

        DisplayName = displayName;
        From = from;
        Password = passWord;
        Port = port;
        SmtpServer = smtpServer;
        UserName = userName;
        _appConfiguration = appConfiguration;
    }

    public string From { get; set; }
    public string SmtpServer { get; set; }
    public string Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string DisplayName { get; set; }
}
