using System.Linq.Expressions;
using API.Login.Domain.Interfaces.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.Login.Infra;

public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity, TContext>
 where TEntity : class
 where TContext : DbContext
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;
    public GenericRepository(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync(IDbContextTransaction transaction)
    {
        await transaction.CommitAsync();
    }

    public void Rollback(IDbContextTransaction transaction)
    {
        transaction.Rollback();
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<List<TEntity>?> GetListAsync(Expression<Func<TEntity, bool>>? filter)
    {
        var query = _dbSet.AsQueryable();

        if (filter is not null)
            query = query.Where(filter).AsNoTracking();

        return await query.ToListAsync();
    }

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? filter)
    {
        var query = _dbSet.AsQueryable();

        if (filter is not null)
            query = query.Where(filter).AsNoTracking();

        return await query.FirstOrDefaultAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAllAsync()
    {
        var allRecords = await GetListAsync(null);
        if (allRecords is not null && allRecords.Count() > 0)
        {
            _context.RemoveRange(allRecords);
            await _context.SaveChangesAsync();
        }
    }


}
