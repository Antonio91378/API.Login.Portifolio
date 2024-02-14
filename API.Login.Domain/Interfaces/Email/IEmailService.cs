using API.Login.Domain.Dtos.Request;
using API.Login.Domain.Dtos.Response;

namespace API.Login.Domain.Interfaces.Email;

public interface IEmailService
{
    Task<ControllerMessenger> SendEmailAsync(EmailRequest mailRequest);
}
