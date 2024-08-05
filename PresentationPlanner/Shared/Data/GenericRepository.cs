using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace PresentationPlanner.Shared.Data;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>, IAsyncDisposable where TEntity : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return await _dbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Deleted;
    }

    public IQueryable<TEntity> GetAll(
    Expression<Func<TEntity, bool>> filter = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    string includeProperties = "",
    bool noTracking = false,
    int? take = null,
    int? skip = null)
    {
        IQueryable<TEntity> query = noTracking ? _dbSet.AsNoTracking() : _dbSet;

        query = filter != null ? query.Where(filter) : query;

        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        query = skip.HasValue ? query.Skip(skip.Value) : query;
        query = take.HasValue ? query.Take(take.Value) : query;

        return orderBy != null ? orderBy(query) : query;
    }

    public async ValueTask<TEntity> GetByKeysAsync(CancellationToken cancellationToken, params object[] keys)
    {
        return await _dbSet.FindAsync(keys, cancellationToken).ConfigureAwait(false);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "",
        bool noTracking = false,
        int? take = null,
        int? skip = null,
        CancellationToken cancellationToken = default)
    {
        return await GetAll(filter, orderBy, includeProperties, noTracking, take, skip)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
    }

}
