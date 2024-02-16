using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Machine
{
    /// <summary>
    /// Фото автомобіля
    /// </summary>
    public class CarPhoto
    {
        [Key]
        /// <summary>
        /// Назва фото або шлях
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Автомобіль
        /// </summary>
        [JsonIgnore]
        public virtual Car Car { get; set; }
    }
}
