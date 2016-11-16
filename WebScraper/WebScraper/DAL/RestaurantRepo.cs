using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using WebScraper.Models;

namespace WebScraper.DAL
{
    public class RestaurantRepo
    {
        RestaurantScrapedDb db = new RestaurantScrapedDb();
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
        Math.Acos(Math.Sin(lat) * Math.Sin((Double)a.Address.Latitude) + Math.Cos(lat) * Math.Cos((Double)a.Address.Latitude) * Math.Cos((Double)a.Address.Longitude - (lon))) <= distance
                               select a).ToList();

            return restaurants;
        }


        /// <summary>
        /// Deletes all values from both addresses and restaurants
        /// </summary>
        public void DeleteTables()
        {
            var restaurants = from c in db.Restaurants select c;
            var addresses = from c in db.Addresses select c;
            db.Restaurants.RemoveRange(restaurants);
            db.Addresses.RemoveRange(addresses);
            db.SaveChanges();
        }

        /// <summary>
        /// Inserts restaurant collection
        /// </summary>
        /// <param name="obj"></param>
        public void InsertRestaurants(IEnumerable<Restaurant> obj)
        {
            try
            {
                db.Restaurants.AddRange(obj);
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

            }
        }

        /// <summary>
        /// This method is used to select states and cities from
        /// a database table in order to pre populate
        /// yelps query string parameters
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,string> GetLocationsToSearchFor()
        {
           
                var query = (from L1 in db.GeoDatas
                    group L1.City by L1.State
                    into stateGroup
                    select new
                    {
                        State = stateGroup.Key,
                        City = stateGroup.Distinct().FirstOrDefault()
                    }).ToDictionary(x => x.State, x => x.City);
                return query;
        }

        /// <summary>
        /// If yelps original query string parameters returned no 
        /// html nodes, then I would retry new state and city values
        /// using this method
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public string RetryNewLocation(string state)
        {
            var query = (from L1 in db.GeoDatas
                         where L1.State == state
                         select new
                         {
                             L1.City
                         }).ToList();
            query.Shuffle();
            return Convert.ToString(query.FirstOrDefault());
        }

      
    }
}
