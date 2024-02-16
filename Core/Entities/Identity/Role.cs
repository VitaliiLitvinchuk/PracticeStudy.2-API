using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    /// <summary>
    /// Розширення ролей
    /// </summary>
    public class Role : IdentityRole<Guid>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
