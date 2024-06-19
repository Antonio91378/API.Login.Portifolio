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

    public static string ReturnRegisterConfirmationHtml()
    {
        return @"

                <!DOCTYPE html>
                <html lang=""en"">
                  <head>
                    <meta charset=""UTF-8"" />
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
                    <title>Document</title>
                  </head>
                  <body>
                    <p>To confirm your email, click <a href=""{link}"">here</a></p>
                    <p>If it didn't work, copy and paste the link {link}</p>
                  </body>
                </html>
                
                ";
    }
}
