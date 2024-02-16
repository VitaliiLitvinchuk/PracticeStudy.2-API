using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core
{
    /// <summary>
    /// Репозиторій
    /// </summary>
    /// <typeparam name="TEntity">Сутність</typeparam>
    /// <param name="context">База даних</param>
    public class Repository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
        /// <summary>
        /// {Interface} Описано в інтерфейсі IRepository
        /// </summary>
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                return await Task.Run(() => _dbSet.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// {Interface} Описано в інтерфейсі IRepository
        /// </summary>
        public async Task<IEnumerable<TEntity>> Get(
                                    Expression<Func<TEntity, bool>> filter = null,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    params Expression<Func<TEntity, object>>[] includes)
        {
            try
            {
                return await Task.Run(() =>
                {
                    IQueryable<TEntity> query = _dbSet;

                    if (filter != null)
                        query = query.Where(filter);

                    foreach (var include in includes)
                        query = query.Include(include);

                    if (orderBy != null)
                        return orderBy(query).ToList();
                    else
                        return [.. query];
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// {Interface} Описано в інтерфейсі IRepository
        /// </summary>
        public async Task<TEntity> GetByKey(params object[] keys)
        {
            try
            {
                return await _dbSet.FindAsync(keys);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// {Interface} Описано в інтерфейсі IRepository
        /// </summary>
        public async Task Insert(TEntity entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// {Interface} Описано в інтерфейсі IRepository
        /// </summary>
        public async Task Update(TEntity entity)
        {
            try
            {
                await Task.Run(() => _dbSet.Update(entity));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// {Interface} Описано в інтерфейсі IRepository
        /// </summary>
        public async Task Delete(params object[] keys)
        {
            try
            {
                TEntity entityToDelete = await _dbSet.FindAsync(keys);

                await Delete(entityToDelete);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// {Interface} Описано в інтерфейсі IRepository
        /// </summary>
        public async Task Delete(TEntity entity)
        {
            try
            {
                await Task.Run(() =>
                {
                    if (_context.Entry(entity).State == EntityState.Detached)
                        _dbSet.Attach(entity);

                    _dbSet.Remove(entity);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// {Interface} Описано в інтерфейсі IRepository
        /// </summary>
        public async Task SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
