using API.Login.Domain.Dtos.Request;
using API.Login.Domain.Dtos.Response;
using API.Login.Domain.Interfaces.Email;
using API.Login.Utils;
using System.Net;
using System.Net.Mail;

namespace API.Login.Service.Email;

public class EmailService : IEmailService
{
    private readonly EmailConfiguration _mailSettings;
    private readonly ControllerMessenger _controllerMessenger = new();

    public EmailService(IAppConfiguration appConfiguration)
    {
        _mailSettings = appConfiguration.GetEmailConfiguration();
    }

    public async Task<ControllerMessenger> SendEmailAsync(EmailRequest mailRequest)
    {
        try
        {
            List<string> emails = mailRequest.ToEmail.Split(';').ToList();
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress(_mailSettings.From, _mailSettings.DisplayName);
            foreach (var item in emails)
            {
                message.To.Add(new MailAddress(item));
            }
            message.Subject = mailRequest.Subject;

            if (mailRequest.Attachments != null)
            {
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            Attachment att = new Attachment(new MemoryStream(fileBytes), file.FileName);
                            if (mailRequest.AttachmentId is not null)
                            {
                                att.ContentId = mailRequest.AttachmentId;
                            }
                            message.Attachments.Add(att);
                        }
                    }
                }
            }

            message.IsBodyHtml = mailRequest.IsBodyHtml;
            message.Body = mailRequest.Body;
            smtp.Port = Convert.ToInt32(_mailSettings.Port);
            smtp.Host = _mailSettings.SmtpServer;
            smtp.EnableSsl = mailRequest.EnableSsl;

            if (mailRequest.Authenticationrequired == true)
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_mailSettings.From, _mailSettings.Password);
            }
            else
            {
                smtp.UseDefaultCredentials = true;
            }
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            await smtp.SendMailAsync(message);

            return _controllerMessenger.ReturnSuccess(200, $"Email enviado com sucesso");
        }
        catch (System.Exception ex)
        {
            return _controllerMessenger.ReturnInternalError500($"{ex.Message}");
        }
    }

}
