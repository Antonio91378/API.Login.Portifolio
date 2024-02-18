using API.Login.Domain.Dtos.Request;
using API.Login.Domain.Dtos.Response;
using API.Login.Domain.Entities.User;
using API.Login.Domain.Interfaces.Service;

namespace API.Login.Service.Users;

public interface IUserService : ICRUDService<User>
{
    Task<ControllerMessenger> RegisterUser(UserRegisterDto user);
    Task<ControllerMessenger> LoginUser(UserLoginDto user);
}
