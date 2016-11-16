namespace WebScraper.App_Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RestaurantScrapedDb : DbContext
    {
        public RestaurantScrapedDb()
            : base("name=RestaurantDbCtx")
        {
        }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<GeoData> GeoDatas { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .Property(e => e.CityStatePostal)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.Street)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.PostalCode)
                .IsUnicode(false);

            modelBuilder.Entity<GeoData>()
                .Property(e => e.PostalCode)
                .IsUnicode(false);

            modelBuilder.Entity<GeoData>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<GeoData>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Restaurant>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Restaurant>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<Restaurant>()
                .Property(e => e.ImageReference)
                .IsUnicode(false);

            modelBuilder.Entity<Restaurant>()
                .Property(e => e.ImageSource)
                .IsUnicode(false);

            modelBuilder.Entity<Restaurant>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);
        }
    }
}
