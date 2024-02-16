using Core.Entities.Machine;
using Core.ViewModels.Machine;

namespace Core.Interfaces.Services.Machine.Part
{
    public interface ICarUpdateService
    {
        Task UpdateCar(CarViewModel model, Guid id);
        Task UpdatePhoto(CarPhotoViewModel model, string name);
        Task UpdateProperty(PropertyViewModel model, string name);
        Task UpdateCharacteristic(CharacteristicViewModel mode, Guid id);
    }
}
