using Core.Entities.Machine;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Identity
{
    /// <summary>
    /// Розширення користувача
    /// </summary>
    public class User : IdentityUser<Guid>
    {
        /// <summary>
        /// Ім'я користувача
        /// </summary>
        [StringLength(100)]
        public string FirstName { get; set; }

        /// <summary>
        /// Прізвище користувача
        /// </summary>
        [StringLength(100)]
        public string SecondName { get; set; }

        /// <summary>
        /// Назва фото
        /// </summary>
        public string Photo { get; set; }

        public virtual List<Car> Cars { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
