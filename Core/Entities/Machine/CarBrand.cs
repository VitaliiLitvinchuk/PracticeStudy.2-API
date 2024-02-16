using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Core.Entities.Machine
{
    /// <summary>
    /// Марка машини
    /// </summary>
    public class CarBrand
    {
        [Key]
        /// <summary>
        /// Назва марки
        /// </summary>
        public string Name { get; set; }
        [JsonIgnore]
        public virtual List<Car> Cars { get; set; } = new List<Car>();
    }
}
