namespace Core.Interfaces.Services.Machine.Part
{
    public interface ICarDeleteService
    {
        Task DeleteProperty(string name);
        Task DeleteCar(Guid id);
        Task DeleteBrand(string name);
        Task DeletePhoto(string name);
        Task DeleteCharacteristic(Guid id);
        Task DeleteYear(int year);
    }
}
