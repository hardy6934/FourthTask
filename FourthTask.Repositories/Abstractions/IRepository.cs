﻿using FourthTask.DataBase; 
using System.Linq.Expressions; 

namespace FourthTask.Data.Repositories.Abstractions
{

    public interface IRepository<T> where T : IBaseEntity
    { 
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Get();

        IQueryable<T> FindBy(Expression<Func<T, bool>> searchExpression,
            params Expression<Func<T, object>>[] includes);
         
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
         
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entity);
 
         
        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);

    }
}
