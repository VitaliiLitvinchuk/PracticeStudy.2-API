using AutoMapper;
using Core.Entities.Machine;
using Core.ViewModels.Machine;
using Core;
using Middleware.Managers.Interfaces;
using System.Linq.Expressions;
using Infrastructure.Data;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Middleware.Managers
{
    public class CarManager(PracticeDBContext context,
                            UserManager<User> userManager,
                            IMapper mapper,
                            ILogger<CarManager> logger,
                            IManager<CarPhotoViewModel, CarPhoto, string> photoManager
                           ) : IManager<CarViewModel, Car, Guid>
    {
        private readonly Repository<Car> _car = new(context);
        private readonly Repository<CarBrand> _carBrand = new(context);
        private readonly Repository<CarYear> _carYear = new(context);
        private readonly UserManager<User> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<CarManager> _logger = logger;
        private readonly IManager<CarPhotoViewModel, CarPhoto, string> _photoManager = photoManager;

        public async Task<IEnumerable<Guid>> AddEntities(params CarViewModel[] models)
        {
            try
            {
                _logger.LogInformation($"Adding cars");
                ArgumentNullException.ThrowIfNull(models);

                List<Guid> ids = [];

                foreach (var model in models)
                {
                    ArgumentNullException.ThrowIfNull(model);

                    Car car = _mapper.Map<Car>(model);

                    car.User = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == model.UserId)
                                          ?? throw new ArgumentException($"Not Found user: {model.UserId}");

                    var years = await _carYear.Get(x => x.YearOfManufacture == model.Year.YearOfManufacture);
                    car.Year = years.Any() ? years.First()
                                           : throw new ArgumentException($"Not Found year: {model.Year.YearOfManufacture}");

                    var brands = await _carBrand.Get(x => x.Name == model.Brand.Name);
                    car.Brand = brands.Any() ? brands.First()
                                             : throw new ArgumentException($"Not Found brand: {model.Brand.Name}");

                    await _car.Insert(car);

                    ids.Add(car.Id);
                }

                await _car.SaveChanges();

                return ids;
            }
            catch (Exception e)
            {
                _logger.LogWarning("Fail adding cars: {message}", e.Message);
                throw;
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                _logger.LogInformation($"Getting cars");
                await _car.Delete(id);
                await _car.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogWarning("Fail deleting cars: {message}", e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Car>> Get(Expression<Func<Car, bool>> filter = null,
                                          Func<IQueryable<Car>, IOrderedQueryable<Car>> orderBy = null,
                                          params Expression<Func<Car, object>>[] includes)
        {
            try
            {
                _logger.LogInformation($"Getting cars");
                if (filter == null && orderBy == null && includes.Length == 0)
                    return await _car.GetAll();

                return await _car.Get(filter, orderBy, includes);
            }
            catch (Exception e)
            {
                _logger.LogWarning("Fail getting cars: {message}", e.Message);
                throw;
            }
        }

        public async Task Update(CarViewModel model, Guid id)
        {
            try
            {
                _logger.LogInformation($"Updating car");
                ArgumentNullException.ThrowIfNull(model);

                Car car = (await _car.Get(x => x.Id == id)).FirstOrDefault()
                                  ?? throw new ArgumentException($"Not Found car: {id}");

                _mapper.Map(model, car);

                if (model.UserId != Guid.Empty)
                    car.User = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == model.UserId)
                                          ?? throw new ArgumentException($"Not Found user: {model.UserId}");


                if (model.Year != null)
                    car.Year = await _carYear.GetByKey(model.Year.YearOfManufacture)
                                        ?? throw new Exception($"Not Found year: {model.Year.YearOfManufacture}");

                if (model.Brand != null)
                    car.Brand = await _carBrand.GetByKey(model.Brand.Name)
                                        ?? throw new Exception($"Not Found brand: {model.Brand.Name}");

                await _car.Update(car);
                await _car.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogWarning("Fail updating car: {message}", e.Message);
                throw;
            }
        }
    }
}
