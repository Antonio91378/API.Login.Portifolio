using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
namespace API.Login.Domain.Interfaces.Infra;

public interface IGenericRepository<TEntity, TContext>
 where TEntity : class
 where TContext : DbContext
{
    Task<List<TEntity>?> GetAsync(Expression<Func<TEntity, bool>>? filter);
    Task<TEntity?> GetByIdAsync(int id);
    Task AddAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task DeleteAllAsync();
    Task UpdateAsync(TEntity entity);
}
