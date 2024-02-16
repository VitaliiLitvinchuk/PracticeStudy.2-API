using Core.ViewModels.Machine;

namespace Core.Interfaces.Services.Machine.Part
{
    public interface ICarAddService
    {
        Task<string> AddProperty(PropertyViewModel model);
        Task<Guid> AddCar(CarViewModel model);
        Task<string> AddBrand(CarBrandViewModel model);
        Task<string> AddPhoto(CarPhotoViewModel model);
        Task<Guid> AddCharacteristic(CharacteristicViewModel model);
        Task<int> AddYear(CarYearViewModel model);
    }
}
