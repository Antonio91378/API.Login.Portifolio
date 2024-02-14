namespace API.Login.Utils;

public class EmailConfiguration
{
    public EmailConfiguration(
        string from,
        string smtpServer,
        string port,
        string userName,
        string passWord,
        string displayName)
    {
        this.DisplayName = displayName;
        this.From = from;
        this.Password = passWord;
        this.Port = port;
        this.SmtpServer = smtpServer;
        this.UserName = userName;
    }

    public string From { get; set; }
    public string SmtpServer { get; set; }
    public string Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string DisplayName { get; set; }
}
