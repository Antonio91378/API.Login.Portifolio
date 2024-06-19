using API.Login.Domain.Interfaces.Infra;
using API.Login.Infra.Contexts;
using API.Login.Domain.Entities;

namespace API.Login.Infra.Users;

public interface IUserRepository : IGenericRepository<User, SqlLiteContext>
{

}
