using OpenLibrary.Domain.Commons;
using OpenLibrary.Data.DbContexts;
using OpenLibrary.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace OpenLibrary.Data.Repositories;


public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly AppDbContext _dbContext;
    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        _dbSet.Remove(entity);

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var model = (await _dbSet.AddAsync(entity)).Entity;
        await _dbContext.SaveChangesAsync();

        return model;
    }

    public IQueryable<TEntity> SelectAll()
        => _dbSet;

    public async Task<TEntity> SelectByIdAsync(long id)
        => await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var model = _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();

        return model.Entity;
    }
}
