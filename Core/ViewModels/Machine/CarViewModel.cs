namespace Core.ViewModels.Machine
{
    /// <summary>
    /// Модель машини
    /// </summary>
    public class CarViewModel
    {
        /// <summary>
        /// Ім'я
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Рік випуску
        /// </summary>
        public CarYearViewModel Year { get; set; }
        /// <summary>
        /// Бренд автомобіля
        /// </summary>
        public CarBrandViewModel Brand { get; set; }
        /// <summary>
        /// Користувач, якому належить
        /// </summary>
        public Guid UserId { get; set; }
    }
}
