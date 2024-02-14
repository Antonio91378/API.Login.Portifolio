using API.Login.Domain.Entities.User;
using API.Login.Infra.Contexts;

namespace API.Login.Infra.Users;

public class UserRepository : GenericRepository<User, SqlLiteContext>, IUserRepository
{
    public UserRepository(SqlLiteContext context) : base(context)
    {
    }
}
