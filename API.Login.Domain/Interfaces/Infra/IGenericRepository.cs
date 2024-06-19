using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
namespace API.Login.Domain.Interfaces.Infra;

public interface IGenericRepository<TEntity, TContext>
 where TEntity : class
 where TContext : DbContext
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitAsync(IDbContextTransaction transaction);
    void Rollback(IDbContextTransaction transaction);
    Task<List<TEntity>?> GetListAsync(Expression<Func<TEntity, bool>>? filter);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? filter);
    Task<TEntity?> GetByIdAsync(int id);
    Task AddAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task DeleteAllAsync();
    Task UpdateAsync(TEntity entity);
}
