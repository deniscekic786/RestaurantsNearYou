using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using PlaceNearYouApp.BLL;
using PlaceNearYouApp.Models;

namespace PlaceNearYouApp.DAL
{
    public class RestaurantRepository
    {
        RestaurantDbCtx db = new RestaurantDbCtx();

        public List<Restaurant> GetAllNearByRestaurants(string state, string city)
        {
            var coordinates = (from g in db.GeoDatas
                               where g.State == state && g.City == city
                               select g).FirstOrDefault();
            var lat = (double)coordinates.Latitude;
            var lon = (double)coordinates.Longitude;
            var location = GeoLocation.FromDegrees(lat, lon);
            GeoLocation[] boundingCoordinates = location.BoundingCoordinates(10);
            var distance = location.DistanceTo(boundingCoordinates[1]);
            var minlat = boundingCoordinates[0].getLatitudeInDegrees();
            var maxlat = boundingCoordinates[1].getLatitudeInDegrees();
            var minlon = boundingCoordinates[0].getLongitudeInDegrees();
            var maxlon = boundingCoordinates[1].getLongitudeInDegrees();

            var restaurants = (from a in db.Restaurants
                               where (a.Address.Latitude >= minlat && a.Address.Latitude <= maxlat) && (a.Address.Longitude >= minlon && a.Address.Longitude <= maxlon) &&
        SqlFunctions.Acos(SqlFunctions.Sin(lat) * SqlFunctions.Sin((Double)a.Address.Latitude) + SqlFunctions.Cos(lat) * SqlFunctions.Cos((Double)a.Address.Latitude) * SqlFunctions.Cos((Double)a.Address.Longitude - (lon))) <= distance
                               select a).ToList();

            return restaurants;
        }

        public void InsertRestaurants(IEnumerable<Restaurant> obj)
        {
        
            db.Restaurants.AddRange(obj);
            db.SaveChanges();
        }
        public List<LocationsViewModel> GetLocationsToSearchFor()
        {
            var query = (from L1 in db.GeoDatas
                         group L1.City by L1.State 
                         into stateGroup
                         select new LocationsViewModel
                         {
                             State = stateGroup.Key,
                             City = stateGroup.Distinct().FirstOrDefault()
                         }).OrderBy(x => x.State).ToList();
            return query;
        }
    }
}