using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer_DAL
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int LocationId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool isActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DealerName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType = DefaultAuthenticationTypes.ApplicationCookie)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, DataAccessLayer_DAL.Migrations.Configuration>());
        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            builder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            builder.Conventions.Remove<PluralizingTableNameConvention>();
            builder.Entity<TenderStatus>().HasIndex(e => e.Type).IsUnique();
            builder.Entity<Manufacturer>().HasIndex(e => e.ManufacturerName).IsUnique();
            builder.Entity<Tender>().HasIndex(t => t.TenderNo).IsUnique();
            builder.Entity<StockInfo>().HasIndex(s => s.RegNo).IsUnique();
            builder.Entity<Location>().HasIndex(l => l.City).IsUnique();
            builder.Entity<Location>().HasIndex(l => l.ZipCode).IsUnique();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<CarModel> CarModel { get; set; }
        public virtual DbSet<StockInfo> StockInfo { get; set; }
        public virtual DbSet<Tender> Tender { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<TenderStatus> TenderStatus { get; set; }
        public virtual DbSet<TenderStock> TenderStock { get; set; }
        public virtual DbSet<TenderUser> TenderUser { get; set; }
        public virtual DbSet<Bid> Bid { get; set; }
    }
}