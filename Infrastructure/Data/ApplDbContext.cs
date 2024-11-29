

using Exchange_data_in_secure_manner.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    internal class ApplDbContext : DbContext
    {
        public ApplDbContext(DbContextOptions<ApplDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> Users { get; set; }
    }
}

