using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Core.Entities.Machine
{
    /// <summary>
    /// Рік випуску машини
    /// </summary>
    public class CarYear
    {
        /// <summary>
        /// Рік випуску
        /// </summary>
        [Key]
        public int YearOfManufacture { get; set; }
        [JsonIgnore]
        public virtual List<Car> Cars { get; set; } = new List<Car>();
    }
}
