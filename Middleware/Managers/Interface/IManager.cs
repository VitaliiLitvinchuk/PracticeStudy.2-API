using Core.Entities.Machine;
using System.Linq.Expressions;

namespace Middleware.Managers.Interfaces
{
    public interface IManager<TViewModel, TEntity, TKey> where TViewModel : class
                                                         where TEntity : class 
    {
        /// <summary>
        /// Додавання
        /// </summary>
        /// <returns>Назва властивості</returns>
        Task<IEnumerable<TKey>> AddEntities(params TViewModel[] models);
        /// <summary>
        /// Видалення
        /// </summary>
        /// <returns>void</returns>
        Task Delete(TKey key);
        /// <summary>
        /// Оновлення
        /// </summary>
        /// <returns>void</returns>
        Task Update(TViewModel model, TKey key);
        /// <summary>
        /// Отримати
        /// </summary>
        /// <param name="filter">Фільтрація (entity) => [>, <, ==, !=...]</param>
        /// <param name="orderBy">Сортування (entity) => entity.[Property]</param>
        /// <param name="includes">Включення вкладених сутностей (includes: [entity => entity.(Property1), entity => entity.(Property2)])</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> Get(
                                   Expression<Func<TEntity, bool>> filter = null,
                                   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                   params Expression<Func<TEntity, object>>[] includes);
    }
}
