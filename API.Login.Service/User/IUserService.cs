using API.Login.Domain.Dtos.Request;
using API.Login.Domain.Dtos.Response;
using API.Login.Domain.Entities;
using API.Login.Domain.Interfaces.Service;

namespace API.Login.Service.Users;

public interface IUserService : ICRUDService<User>
{
    Task<ControllerMessenger> RegisterUserAsync(UserRegisterDto user);
    // Task<ControllerMessenger> LoginUser(UserLoginDto user);
    Task<ControllerMessenger> ConfirmUserRegistrationAsync(UserRegisterConfirmationDto user);
}
