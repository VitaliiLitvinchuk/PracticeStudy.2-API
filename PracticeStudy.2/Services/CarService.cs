using Core.Entities.Machine;
using Core.Interfaces.Services.Machine;
using Core.Pagination;
using Core.ViewModels.Machine;
using Middleware.Managers;
using Middleware.Managers.Interfaces;

namespace PracticeStudy._2.Services
{
    public class CarService : ICarService
    {
        private readonly IManager<PropertyViewModel, Property, string> _propertyManager;
        private readonly IManager<CharacteristicViewModel, Characteristic, Guid> _characteristicManager;
        private readonly IManager<CarBrandViewModel, CarBrand, string> _brandManager;
        private readonly IManager<CarYearViewModel, CarYear, int> _yearManager;
        private readonly IManager<CarPhotoViewModel, CarPhoto, string> _photoManager;
        private readonly IManager<CarViewModel, Car, Guid> _carManager;

        public CarService(IManager<PropertyViewModel, Property, string> propertyManager,
                          IManager<CharacteristicViewModel, Characteristic, Guid> characteristicManager,
                          IManager<CarBrandViewModel, CarBrand, string> brandManager,
                          IManager<CarYearViewModel, CarYear, int> yearManager,
                          IManager<CarPhotoViewModel, CarPhoto, string> photoManager,
                          IManager<CarViewModel, Car, Guid> carManager)
        {
            _propertyManager = propertyManager;
            _characteristicManager = characteristicManager;
            _brandManager = brandManager;
            _yearManager = yearManager;
            _photoManager = photoManager;
            _carManager = carManager;

            (_yearManager as SmallManager<CarYearViewModel, CarYear, int>).Key = "YearOfManufacture";
        }

        public async Task<string> AddBrand(CarBrandViewModel model)
        {
            return (await _brandManager.AddEntities(model)).First();
        }

        public async Task<Guid> AddCar(CarViewModel model)
        {
            return (await _carManager.AddEntities(model)).First();
        }

        public async Task<Guid> AddCharacteristic(CharacteristicViewModel model)
        {
            return (await _characteristicManager.AddEntities(model)).First();
        }

        public async Task<string> AddPhoto(CarPhotoViewModel model)
        {
            return (await _photoManager.AddEntities(model)).First();
        }

        public async Task<string> AddProperty(PropertyViewModel model)
        {
            return (await _propertyManager.AddEntities(model)).First();
        }

        public async Task<int> AddYear(CarYearViewModel model)
        {
            return (await _yearManager.AddEntities(model)).First();
        }

        public async Task DeleteBrand(string name)
        {
            await _brandManager.Delete(name);
        }

        public async Task DeleteCar(Guid id)
        {
            await _carManager.Delete(id);
        }

        public async Task DeleteCharacteristic(Guid id)
        {
            await _characteristicManager.Delete(id);
        }

        public async Task DeletePhoto(string name)
        {
            await _photoManager.Delete(name);
        }

        public async Task DeleteProperty(string name)
        {
            await _propertyManager.Delete(name);
        }

        public async Task DeleteYear(int year)
        {
            await _yearManager.Delete(year);
        }

        public async Task<IEnumerable<CarBrand>> GetBrands()
        {
            return await _brandManager.Get();
        }

        public async Task<IEnumerable<Car>> GetCarsByUserId(Guid userId)
        {
            return await _carManager.Get(x => x.User.Id == userId);
        }

        public async Task<IEnumerable<Characteristic>> GetCharacteristics(Guid carId)
        {
            return await _characteristicManager.Get(x => x.Car.Id == carId);
        }

        public async Task<IEnumerable<CarPhoto>> GetPhotos(Guid carId)
        {
            return await _photoManager.Get(x => x.Car.Id == carId);
        }

        public async Task<IEnumerable<Property>> GetProperties()
        {
            return await _propertyManager.Get();
        }

        public async Task<IEnumerable<CarYear>> GetYears()
        {
            return await _yearManager.Get();
        }

        public async Task UpdateCar(CarViewModel model, Guid id)
        {
            await _carManager.Update(model, id);
        }

        public async Task UpdateCharacteristic(CharacteristicViewModel model, Guid id)
        {
            await _characteristicManager.Update(model, id);
        }

        public async Task UpdateProperty(PropertyViewModel model, string name)
        {
            await _propertyManager.Update(model, name);
        }

        public async Task UpdatePhoto(CarPhotoViewModel model, string name)
        {
            await _photoManager.Update(model, name);
        }

        public async Task<IEnumerable<Car>> GetCars(CarPagination pagination)
        {
            return (await _carManager.Get(x => (pagination.Year == null || pagination.Year == x.Year.YearOfManufacture)
                                                && (pagination.BrandName == null || pagination.BrandName == x.Brand.Name)))
                                     .Skip(pagination.PageSize * (pagination.Page - 1))
                                     .Take(pagination.PageSize);
        }
    }
}
