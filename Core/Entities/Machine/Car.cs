using Core.Entities.Identity;
using Newtonsoft.Json;

namespace Core.Entities.Machine
{
    /// <summary>
    /// Автомобіль
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Ім'я
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Рік випуску
        /// </summary>
        public virtual CarYear Year { get; set; }
        /// <summary>
        /// Бренд автомобіля
        /// </summary>
        public virtual CarBrand Brand { get; set; }
        /// <summary>
        /// Користувач, якому належить
        /// </summary>
        [JsonIgnore]
        public virtual User User { get; set; }
        public virtual List<CarPhoto> Photos { get; set; } = new List<CarPhoto>();
        public virtual List<Characteristic> Characteristics { get; set; } = new List<Characteristic>();
    }
}
