using API.Login.Domain.Dtos.Request;
using API.Login.Domain.Dtos.Response;
using API.Login.Domain.Entities.User;
using API.Login.Domain.Interfaces.Infra;
using API.Login.Infra.Contexts;

namespace API.Login.Infra.Users;

public interface IUserRepository : IGenericRepository<User, SqlLiteContext>
{

}
