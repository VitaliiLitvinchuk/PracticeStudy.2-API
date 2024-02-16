using Core.Entities.Identity;
using Core.Entities.Machine;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class PracticeDBContext(DbContextOptions<PracticeDBContext> options) : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>,
           UserRole, IdentityUserLogin<Guid>,
           IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>(options)
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarPhoto> CarPhotos { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<CarBrand> Brands { get; set; }
        public DbSet<CarYear> Years { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }
    }
}
