using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    /// <summary>
    /// Взаємодія користувачів з їхніми ролями
    /// </summary>
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
