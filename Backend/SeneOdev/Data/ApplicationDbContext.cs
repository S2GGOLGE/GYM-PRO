using Microsoft.EntityFrameworkCore;
using SeneOdev.Models;

namespace SeneOdev.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<user> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Theame> Theames { get; set; }
    }
}
