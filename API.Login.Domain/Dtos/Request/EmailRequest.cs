using Microsoft.AspNetCore.Http;

namespace API.Login.Domain.Dtos.Request;

public class EmailRequest
{
    public EmailRequest(
        bool enableSsl,
        bool authenticationrequired,
        bool isBodyHtml,
        string toEmail,
        string subject,
        string body)
    {
        this.EnableSsl = enableSsl;
        this.Authenticationrequired = authenticationrequired;
        this.IsBodyHtml = isBodyHtml;
        this.ToEmail = toEmail;
        this.Subject = subject;
        this.Body = body;
    }


    public bool EnableSsl { get; set; }
    public bool Authenticationrequired { get; set; }
    public bool IsBodyHtml { get; set; }
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<IFormFile>? Attachments { get; set; }
    public string? AttachmentId { get; set; }
}
