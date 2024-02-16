using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Core.Entities.Machine
{
    /// <summary>
    /// Властивість
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Назва властивості
        /// </summary>
        [Key]
        public string Name { get; set; }
        /// <summary>
        /// Опис властивості
        /// </summary>
        public string Description { get; set; }
        [JsonIgnore]
        public virtual List<Characteristic> Characteristics { get; set; } = new List<Characteristic>();
    }
}
