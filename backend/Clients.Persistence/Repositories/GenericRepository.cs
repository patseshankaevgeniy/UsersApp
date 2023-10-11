using Users.Application.Common.Interfaces;
using Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Users.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly IApplicationDbContext _db;
    private readonly DbSet<TEntity> _entities;

    public GenericRepository(IApplicationDbContext db)
    {
        _db = db;
        _entities = _db.Set<TEntity>();
    }

    public async Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
    {
        var query = _entities.AsQueryable();
        if (include != null)
        {
            query = include.Invoke(query);
        }
        return await query
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<TEntity> GetAsync(int id)
    {
        return await _entities
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
    {
        var query = _entities.AsQueryable();
        query = query.Where(predicate);
        if (include != null)
        {
            query = include.Invoke(query);
        }
        return await query
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
    {
        var query = _entities.AsQueryable();
        query = query.Where(predicate);
        if (include != null)
        {
            query = include.Invoke(query);
        }
        return await query
                .FirstOrDefaultAsync();
    }

    public async Task<TEntity> CreateAsync(TEntity item, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
    {
        _entities.Add(item);
        await _db.SaveChangesAsync();

        return item;
    }

    public async Task<TEntity> UpdateAsync(TEntity item)
    {
        _entities.Update(item);
        await _db.SaveChangesAsync();

        return item;
    }

    public async Task DeleteAsync(TEntity item)
    {
        _entities.Remove(item);
        await _db.SaveChangesAsync();
    }
}
