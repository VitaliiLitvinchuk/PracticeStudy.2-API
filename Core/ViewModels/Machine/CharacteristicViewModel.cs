namespace Core.ViewModels.Machine
{
    /// <summary>
    /// Модель характеристики
    /// </summary>
    public class CharacteristicViewModel
    {
        /// <summary>
        /// Значення характеристики
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Назва властивості 
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// Автомобіль
        /// </summary>
        public Guid CarId { get; set; }
    }
}
