using API.Login.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Login.Infra.Contexts;

public class SqlLiteContext : DbContext
{
    public SqlLiteContext(DbContextOptions<SqlLiteContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

}
