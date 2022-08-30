using Locker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Locker.API.Data
{
    public class LockersDbContext : DbContext
    {
        public LockersDbContext(DbContextOptions options) : base(options)
        {
        }

        // DbSet
        public DbSet<LockerInfo> Lockers { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
