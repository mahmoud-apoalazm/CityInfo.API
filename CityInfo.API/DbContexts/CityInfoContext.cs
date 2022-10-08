using CityInfo.API.Entites;
using Microsoft.EntityFrameworkCore;
namespace CityInfo.API.DbContexts
{
    public class CityInfoContext :DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointOfInterests { get; set; }=null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("New York City") {Id=1,Description="the one with the pig park" },
                new City("Antwerp") { Id = 2, Description = "the one with the pig park" },
                new City("Paris") { Id = 3, Description = "the one with the pig park" }
                );
            modelBuilder.Entity<PointOfInterest>().HasData(
             new PointOfInterest("Center Park") { Id = 1,CityId=1, Description = "the one with the pig park" },
             new PointOfInterest("Empire") { Id = 2, CityId = 2, Description = "the one with the pig park" },
             new PointOfInterest("Cathedra") { Id = 3, CityId = 3, Description = "the one with the pig park" }

             );

            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectingstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
