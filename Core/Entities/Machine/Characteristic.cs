using Newtonsoft.Json;

namespace Core.Entities.Machine
{
    /// <summary>
    /// Характеристика
    /// </summary>
    public class Characteristic
    {
        /// <summary>
        /// Ідентифікатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Значення характеристики
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Властивість
        /// </summary>
        public virtual Property Property { get; set; }
        /// <summary>
        /// Автомобіль
        /// </summary>
        [JsonIgnore]
        public virtual Car Car { get; set; }
    }
}
