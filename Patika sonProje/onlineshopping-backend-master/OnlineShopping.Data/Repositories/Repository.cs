using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineShopping.Data.Repositories
{
    // Implements generic CRUD operations for entities of type T
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        // Reference to the database context
        protected readonly OnlineShoppingDbContext _context;

        // Reference to the specific DbSet for the entity
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(OnlineShoppingDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // Retrieves all records asynchronously
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Retrieves a record by its id asynchronously
        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Adds a new record asynchronously
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // Updates an existing record
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        // Deletes a record
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        // Retrieves records that match the given predicate asynchronously
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
