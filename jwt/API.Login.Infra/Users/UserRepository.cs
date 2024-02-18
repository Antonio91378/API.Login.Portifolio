using API.Login.Domain.Dtos.Request;
using API.Login.Domain.Dtos.Response;
using API.Login.Domain.Entities.User;
using API.Login.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace API.Login.Infra.Users;

public class UserRepository : GenericRepository<User, SqlLiteContext>, IUserRepository
{
    private readonly SqlLiteContext _context;
    public UserRepository(SqlLiteContext context) : base(context)
    {
        _context = context;
    }
}
