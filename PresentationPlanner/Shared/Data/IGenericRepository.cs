﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace PresentationPlanner.Shared.Data;

public interface IGenericRepository<TEntity> where TEntity : class
{
    ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    ValueTask<TEntity> GetByKeysAsync(CancellationToken cancellationToken, params object[] keys);

    IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "",
        bool noTracking = false, int? take = null, int? skip = null);

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "",
        bool noTracking = false, int? take = null, int? skip = null,
        CancellationToken cancellationToken = default);

    Task<bool> CommitAsync(CancellationToken cancellationToken);
}
