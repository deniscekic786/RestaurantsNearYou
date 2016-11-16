using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.GeoExtentions;

namespace WebScraper
{
 public class GeoLocation
    {
        private double radLat;  // latitude in radians
        private double radLon;  // longitude in radians

        private double degLat;  // latitude in degrees
        private double degLon;  // longitude in degrees

        private static double MIN_LAT = Helper.ConvertDegreesToRadians(-90d);  // -PI/2
        private static double MAX_LAT = Helper.ConvertDegreesToRadians(90d);   //  PI/2
        private static double MIN_LON = Helper.ConvertDegreesToRadians(-180d); // -PI
        private static double MAX_LON = Helper.ConvertDegreesToRadians(180d);  //  PI

        private const double earthRadius = 6371.01;

        private GeoLocation()
        {
        }

        /// <summary>
        /// Return GeoLocation from Degrees
        /// </summary>
        /// <param name="latitude">The latitude, in degrees.</param>
        /// <param name="longitude">The longitude, in degrees.</param>
        /// <returns>GeoLocation in Degrees</returns>
        public static GeoLocation FromDegrees(double latitude, double longitude)
        {
            GeoLocation result = new GeoLocation
            {
                radLat = Helper.ConvertDegreesToRadians(latitude),
                radLon = Helper.ConvertDegreesToRadians(longitude),
                degLat = latitude,
                degLon = longitude
            };
            result.CheckBounds();
            return result;
        }

        /// <summary>
        /// Return GeoLocation from Radians
        /// </summary>
        /// <param name="latitude">The latitude, in radians.</param>
        /// <param name="longitude">The longitude, in radians.</param>
        /// <returns>GeoLocation in Radians</returns>
        public static GeoLocation FromRadians(double latitude, double longitude)
        {
            GeoLocation result = new GeoLocation
            {
                radLat = latitude,
                radLon = longitude,
                degLat = Helper.ConvertRadiansToDegrees(latitude),
                degLon = Helper.ConvertRadiansToDegrees(longitude)
            };
            result.CheckBounds();
            return result;
        }

        private void CheckBounds()
        {
            if (radLat < MIN_LAT || radLat > MAX_LAT ||
                    radLon < MIN_LON || radLon > MAX_LON)
                throw new Exception("Arguments are out of bounds");
        }

        /**
         * @return the latitude, in degrees.
         */
        public double getLatitudeInDegrees()
        {
            return degLat;
        }

        /**
         * @return the longitude, in degrees.
         */
        public double getLongitudeInDegrees()
        {
            return degLon;
        }

        /**
         * @return the latitude, in radians.
         */
        public double getLatitudeInRadians()
        {
            return radLat;
        }

        /**
         * @return the longitude, in radians.
         */
        public double getLongitudeInRadians()
        {
            return radLon;
        }

        public override string ToString()
        {
            return "(" + degLat + "\u00B0, " + degLon + "\u00B0) = (" +
                     radLat + " rad, " + radLon + " rad)";
        }

        /// <summary>
        /// Computes the great circle distance between this GeoLocation instance and the location argument.
        /// </summary>
        /// <param name="location">Location to act as the centre point</param>
        /// <returns>the distance, measured in the same unit as the radius argument.</returns>
        public double DistanceTo(GeoLocation location)
        {
            return Math.Acos(Math.Sin(radLat) * Math.Sin(location.radLat) +
                    Math.Cos(radLat) * Math.Cos(location.radLat) *
                    Math.Cos(radLon - location.radLon)) * earthRadius;
        }
        
        public GeoLocation[] BoundingCoordinates(double distance)
        {

            if (distance < 0d)
                throw new Exception("Distance cannot be less than 0");
            // angular distance in radians on a great circle
            double radDist = distance / earthRadius;

            double minLat = radLat - radDist;
            double maxLat = radLat + radDist;

            double minLon, maxLon;
            if (minLat > MIN_LAT && maxLat < MAX_LAT)
            {
                double deltaLon = Math.Asin(Math.Sin(radDist) /
                    Math.Cos(radLat));
                minLon = radLon - deltaLon;
                if (minLon < MIN_LON) minLon += 2d * Math.PI;
                maxLon = radLon + deltaLon;
                if (maxLon > MAX_LON) maxLon -= 2d * Math.PI;
            }
            else
            {
                // a pole is within the distance
                minLat = Math.Max(minLat, MIN_LAT);
                maxLat = Math.Min(maxLat, MAX_LAT);
                minLon = MIN_LON;
                maxLon = MAX_LON;
            }

            return new GeoLocation[]
            {
                FromRadians(minLat, minLon),
                FromRadians(maxLat, maxLon)
            };
        }
    }
}
