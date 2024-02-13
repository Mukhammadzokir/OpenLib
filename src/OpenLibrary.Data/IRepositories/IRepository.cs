﻿using OpenLibrary.Domain.Commons;

namespace OpenLibrary.Data.IRepositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    IQueryable<TEntity> SelectAll();
    Task<bool> DeleteAsync(long id);
    Task<TEntity> SelectByIdAsync(long id);
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
}
