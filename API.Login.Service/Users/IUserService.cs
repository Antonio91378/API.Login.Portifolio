using API.Login.Domain.Entities.User;
using API.Login.Domain.Interfaces.Service;

namespace API.Login.Service.Users;

public interface IUserService : ICRUDService<User>
{
}
