using Microsoft.EntityFrameworkCore;
using MiniApi.Entities;

namespace MiniApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Raccoon> Raccoons { get; set; }
    }
}
