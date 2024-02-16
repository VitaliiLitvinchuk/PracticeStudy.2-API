using Core.Entities.Machine;
using Core.Pagination;

namespace Core.Interfaces.Services.Machine.Part
{
    public interface ICarGetService
    {
        Task<IEnumerable<Property>> GetProperties();
        Task<IEnumerable<Car>> GetCarsByUserId(Guid userId);
        Task<IEnumerable<Car>> GetCars(CarPagination pagination);
        Task<IEnumerable<CarBrand>> GetBrands();
        Task<IEnumerable<CarPhoto>> GetPhotos(Guid carId);
        Task<IEnumerable<Characteristic>> GetCharacteristics(Guid carId);
        Task<IEnumerable<CarYear>> GetYears();
    }
}
