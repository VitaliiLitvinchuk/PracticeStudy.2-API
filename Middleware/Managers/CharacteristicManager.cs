using AutoMapper;
using Core.Entities.Machine;
using Core.ViewModels.Machine;
using Core;
using Infrastructure.Data;
using Middleware.Managers.Interfaces;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace Middleware.Managers
{
    public class CharacteristicManager(PracticeDBContext context, IMapper mapper, ILogger<CharacteristicManager> logger) : IManager<CharacteristicViewModel, Characteristic, Guid>
    {
        private readonly Repository<Car> _car = new(context);
        private readonly Repository<Property> _property = new(context);
        private readonly Repository<Characteristic> _characteristic = new(context);
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<CharacteristicManager> _logger = logger;

        public async Task<IEnumerable<Guid>> AddEntities(params CharacteristicViewModel[] models)
        {
            try
            {
                _logger.LogInformation($"Adding characteristics");
                ArgumentNullException.ThrowIfNull(models);

                List<Guid> ids = [];

                foreach (var model in models)
                {
                    ArgumentNullException.ThrowIfNull(model);

                    Characteristic characteristic = _mapper.Map<Characteristic>(model);

                    characteristic.Car = (await _car.Get(x => x.Id == model.CarId)).FirstOrDefault()
                                                 ?? throw new ArgumentException($"Not Found car: {model.CarId}");

                    characteristic.Property = (await _property.Get(x => x.Name == model.PropertyName)).FirstOrDefault()
                                                         ?? throw new ArgumentException($"Not Found property: {model.CarId}");

                    await _characteristic.Insert(characteristic);

                    ids.Add(characteristic.Id);
                }

                await _characteristic.SaveChanges();

                return ids;
            }
            catch (Exception e)
            {
                _logger.LogWarning("Fail adding characteristics: {message}", e.Message);
                throw;
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                _logger.LogInformation($"Deleting characteristic");
                await _characteristic.Delete(id);
                await _characteristic.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogWarning("Fail deleting characteristic: {message}", e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Characteristic>> Get(Expression<Func<Characteristic, bool>> filter = null,
                                          Func<IQueryable<Characteristic>, IOrderedQueryable<Characteristic>> orderBy = null,
                                          params Expression<Func<Characteristic, object>>[] includes)
        {
            try
            {
                _logger.LogInformation($"Getting characteristic");
                if (filter == null && orderBy == null && includes.Length == 0)
                    return await _characteristic.GetAll();

                return await _characteristic.Get(filter, orderBy, includes);
            }
            catch (Exception e)
            {
                _logger.LogWarning("Fail getting characteristic: {message}", e.Message);
                throw;
            }
        }

        public async Task Update(CharacteristicViewModel model, Guid id)
        {
            try
            {
                _logger.LogInformation($"Updating characteristic");
                ArgumentNullException.ThrowIfNull(model);

                Characteristic characteristic = (await _characteristic.Get(x => x.Id == id)).FirstOrDefault()
                                                             ?? throw new ArgumentException($"Not Found characteristic: {id}");

                _mapper.Map(model, characteristic);

                if (model.CarId != Guid.Empty)
                    characteristic.Car = (await _car.Get(x => x.Id == model.CarId)).FirstOrDefault()
                                                 ?? throw new ArgumentException($"Not Found car: {model.CarId}");

                if (!string.IsNullOrEmpty(model.PropertyName))
                    characteristic.Property = (await _property.Get(x => x.Name == model.PropertyName)).FirstOrDefault()
                                                         ?? throw new ArgumentException($"Not Found property: {model.PropertyName}");

                await _characteristic.Update(characteristic);
                await _characteristic.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogWarning("Fail adding characteristic: {message}", e.Message);
                throw;
            }
        }
    }
}
