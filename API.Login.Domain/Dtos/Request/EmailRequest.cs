using Microsoft.AspNetCore.Http;

namespace API.Login.Domain.Dtos.Request;

public class EmailRequest(
    bool enableSsl,
    bool authenticationrequired,
    bool isBodyHtml,
    string toEmail,
    string subject,
    string body)
{
    public bool EnableSsl { get; set; } = enableSsl;
    public bool Authenticationrequired { get; set; } = authenticationrequired;
    public bool IsBodyHtml { get; set; } = isBodyHtml;
    public string ToEmail { get; set; } = toEmail;
    public string Subject { get; set; } = subject;
    public string Body { get; set; } = body;
    public List<IFormFile>? Attachments { get; set; }
    public string? AttachmentId { get; set; }

    public static EmailRequest CreateDefaultObject(string toEmail, string subject, string body)
    {
        return new EmailRequest(
            enableSsl: true,
            authenticationrequired: true,
            isBodyHtml: true,
            toEmail: toEmail,
            subject: subject,
            body: body);
    }
}
