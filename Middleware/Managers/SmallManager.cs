using AutoMapper;
using Core;
using Middleware.Managers.Interfaces;
using System.Linq.Expressions;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Middleware.Managers
{
    public class SmallManager<TViewModel, TEntity, TKey>(PracticeDBContext context, IMapper mapper, ILogger<SmallManager<TViewModel, TEntity, TKey>> logger)
                                                         : IManager<TViewModel, TEntity, TKey>
                                                         where TViewModel : class
                                                         where TEntity : class
    {
        private readonly Repository<TEntity> _entity = new(context);
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<SmallManager<TViewModel, TEntity, TKey>> _logger = logger;

        public string Key { get; set; } = "Name";

        public async Task<IEnumerable<TKey>> AddEntities(params TViewModel[] models)
        {
            try
            {
                _logger.LogInformation("Adding {type}s", typeof(TEntity).Name);
                ArgumentNullException.ThrowIfNull(models);

                List<TKey> keys = [];

                foreach (var model in models)
                {
                    ArgumentNullException.ThrowIfNull(model);

                    TEntity entity = _mapper.Map<TEntity>(model);
                    await _entity.Insert(entity);

                    keys.Add((TKey)context.Entry(entity).Property(Key).CurrentValue);
                }

                await _entity.SaveChanges();

                return keys;
            }
            catch (Exception e)
            {
                _logger.LogWarning("Fail adding {type}s: {message}", typeof(TEntity).Name, e.Message);
                throw;
            }
        }

        public async Task Delete(TKey key)
        {
            try
            {
                _logger.LogInformation("Deleting {type}", typeof(TEntity).Name);
                ArgumentNullException.ThrowIfNull(key);

                await _entity.Delete(key);
                await _entity.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogWarning("Fail deleting {type}: {message}", typeof(TEntity).Name, e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null,
                                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                     params Expression<Func<TEntity, object>>[] includes)
        {
            try
            {
                _logger.LogInformation("Getting {type}s", typeof(TEntity).Name);

                if (filter == null && orderBy == null && includes.Length == 0)
                    return await _entity.GetAll();

                return await _entity.Get(filter, orderBy, includes);
            }
            catch (Exception e)
            {
                _logger.LogWarning("Fail getting {type}s: {message}", typeof(TEntity).Name, e.Message);
                throw;
            }
        }

        public async Task Update(TViewModel model, TKey key)
        {
            try
            {
                _logger.LogInformation("Updating {type}", typeof(TEntity).Name);
                ArgumentNullException.ThrowIfNull(model);

                await _entity.Update(_mapper.Map(model, await _entity.GetByKey(key)
                                                                 ?? throw new ArgumentException($"Not Found key: {key}")));
                await _entity.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogWarning("Fail updating {type}: {message}", typeof(TEntity).Name, e.Message);
                throw;
            }
        }
    }
}
