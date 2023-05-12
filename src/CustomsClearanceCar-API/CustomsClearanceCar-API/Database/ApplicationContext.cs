using CustomsClearanceCar_API.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomsClearanceCar_API.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<EngineCapacity> EngineCapacities { get; set; }
        public DbSet<Price> Prices { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}
