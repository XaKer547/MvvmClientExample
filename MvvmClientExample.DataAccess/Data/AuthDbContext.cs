using Microsoft.EntityFrameworkCore;
using MvvmClientExample.DataAccess.Data.Entities;

namespace MvvmClientExample.DataAccess.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
