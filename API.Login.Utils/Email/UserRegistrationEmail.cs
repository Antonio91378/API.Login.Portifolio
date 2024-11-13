using API.Login.Domain.Interfaces.Email;

namespace API.Login.Utils.Email
{
    public class UserRegistrationEmail : IUserRegistrationEmail
    {
        private readonly IAppConfiguration _appConfiguration;
        public UserRegistrationEmail(IAppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public string ReturnRegisterConfirmationHtml(string emailHash)
        {
            var registerConfirmationLink = _appConfiguration.ReturnRegisterConfirmationLink();
            return @$"
            <!DOCTYPE html>
            <html lang=""en"">
                <head>
                <meta charset=""UTF-8"" />
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
                <title>Document</title>
                </head>
                <body>
                <p>To confirm your email, click <a href=""{registerConfirmationLink}/{emailHash}"">here</a></p>
                <p>If it didn't work, copy and paste the link {registerConfirmationLink}/{emailHash}</p>
                </body>
            </html>";
        }
    }
}
