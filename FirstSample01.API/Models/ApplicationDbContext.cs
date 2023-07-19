using FirstSample01.API.Models.DomainAggregates;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection;
using FirstSample01.API.Models.Services.Statuses;

namespace FirstSample01.API.Models
{
    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    //    {
    //    }
        public class ApplicationDbContext : DbContext
        {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        internal RepositoryStatus DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public DbSet<Product> Product { get; set; }
    }
}
