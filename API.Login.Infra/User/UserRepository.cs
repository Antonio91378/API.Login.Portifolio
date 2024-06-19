using API.Login.Domain.Entities;
using API.Login.Infra.Contexts;

namespace API.Login.Infra.Users;

public class UserRepository : GenericRepository<User, SqlLiteContext>, IUserRepository
{
    private readonly SqlLiteContext _context;
    public UserRepository(SqlLiteContext context) : base(context)
    {
        _context = context;
    }
}
