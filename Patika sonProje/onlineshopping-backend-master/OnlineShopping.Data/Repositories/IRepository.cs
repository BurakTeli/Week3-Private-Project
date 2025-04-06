using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineShopping.Data.Repositories
{
    // Generic repository interface for performing common data operations on any entity type.
    public interface IGenericRepository<T> where T : class
    {
        // Retrieves all entities from the database.
        Task<IEnumerable<T>> GetAllAsync();

        // Retrieves a single entity by its unique identifier.
        Task<T> GetByIdAsync(object id);

        // Adds a new entity to the database.
        Task AddAsync(T entity);

        // Updates an existing entity.
        void Update(T entity);

        // Deletes an existing entity.
        void Delete(T entity);

        // Finds entities based on a given predicate.
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
