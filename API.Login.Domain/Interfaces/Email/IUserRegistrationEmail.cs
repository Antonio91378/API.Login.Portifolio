namespace API.Login.Domain.Interfaces.Email
{
    public interface IUserRegistrationEmail
    {
        string ReturnRegisterConfirmationHtml(string emailHash);
    }
}
