using AutoMapper;
using Core.Entities.Machine;
using Core.ViewModels.Machine;
using Core;
using Middleware.Managers.Interfaces;
using System.Linq.Expressions;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Middleware.Managers
{
    public class CarPhotoManager(PracticeDBContext context, IMapper mapper, ILogger<CarPhotoManager> logger) : IManager<CarPhotoViewModel, CarPhoto, string>
    {
        private readonly Repository<Car> _car = new(context);
        private readonly Repository<CarPhoto> _photo = new(context);
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<CarPhotoManager> _logger = logger;

        public async Task<IEnumerable<string>> AddEntities(params CarPhotoViewModel[] models)
        {
            try
            {
                _logger.LogInformation($"Adding photos");
                ArgumentNullException.ThrowIfNull(models);

                List<string> names = [];

                foreach (var model in models)
                {
                    ArgumentNullException.ThrowIfNull(model);

                    CarPhoto photo = _mapper.Map<CarPhoto>(model);

                    var car = await _car.Get(x => x.Id == model.CarId);
                    photo.Car = car.Any() ? car.First()
                                          : throw new ArgumentException($"Not Found car: {model.CarId}");

                    await _photo.Insert(photo);

                    names.Add(photo.Name);
                }

                await _photo.SaveChanges();

                return names;
            }
            catch (Exception e)
            {
                _logger.LogWarning("Fail add photos: {message}", e.Message);
                throw;
            }
        }

        public async Task Delete(string name)
        {
            try
            {
                _logger.LogInformation($"Deleting photos");
                ArgumentNullException.ThrowIfNull(name);

                File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "uploads", name));

                await _photo.Delete(name);
                await _photo.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogWarning("Fail deleting photos: {message}", e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<CarPhoto>> Get(Expression<Func<CarPhoto, bool>> filter = null,
                                          Func<IQueryable<CarPhoto>, IOrderedQueryable<CarPhoto>> orderBy = null,
                                          params Expression<Func<CarPhoto, object>>[] includes)
        {
            try
            {
                _logger.LogInformation($"Getting photos");
                if (filter == null && orderBy == null && includes.Length == 0)
                    return await _photo.GetAll();

                return await _photo.Get(filter, orderBy, includes);
            }
            catch (Exception e)
            {
                _logger.LogWarning("Fail getting photos: {message}", e.Message);
                throw;
            }
        }

        public async Task Update(CarPhotoViewModel model, string name)
        {
            try
            {
                _logger.LogInformation($"Updating photos");
                ArgumentNullException.ThrowIfNull(model);

                var photoExtra = await _photo.Get(x => x.Name == name);
                CarPhoto photo = photoExtra.Any() ? photoExtra.First()
                                              : throw new ArgumentException($"Not Found photo: {name}");

                _mapper.Map(model, photo);

                if (model.CarId != Guid.Empty)
                    photo.Car = (await _car.Get(x => x.Id == model.CarId)).FirstOrDefault()
                                        ?? throw new ArgumentException($"Not Found car: {model.CarId}");


                await _photo.Update(photo);
                await _photo.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogWarning("Fail updating photos: {message}", e.Message);
                throw;
            }
        }
    }
}
