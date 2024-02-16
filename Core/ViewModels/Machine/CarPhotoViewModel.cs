using Core.Entities.Machine;

namespace Core.ViewModels.Machine
{
    /// <summary>
    /// Модель фото для автомобіля
    /// </summary>
    public class CarPhotoViewModel
    {
        /// <summary>
        /// Фото (base64)
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// Автомобіль
        /// </summary>
        public Guid CarId { get; set; }
    }
}
