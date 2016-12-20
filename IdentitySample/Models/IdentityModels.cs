using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentitySample.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Display(Name = "FirstName", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(IdentitySample.Resx.AppResource))]
        public string LastName { get; set; }

        public string FullName
        {
            get 
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
#if DEBUG
        public ApplicationDbContext()
            : base("IdentityConnection", throwIfV1Schema: false)
        {
        }
#else
        public ApplicationDbContext()
            : base("YuliConnection", throwIfV1Schema: false)
        {
        }
#endif
        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}