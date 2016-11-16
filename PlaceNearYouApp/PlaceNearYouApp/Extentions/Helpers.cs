using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaceNearYouApp.Extentions
{
    public static class Helper
    {
        public static double ConvertDegreesToRadians(double degrees)
        {
            return (Math.PI / 180) * degrees;
        }

        public static double ConvertRadiansToDegrees(double radian)
        {
            return radian * (180.0 / Math.PI);
        }
    }
}