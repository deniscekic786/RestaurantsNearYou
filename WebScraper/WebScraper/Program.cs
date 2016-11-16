using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.App_Data;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
              //  var repo = new RestaurantRepo();
              //  repo.deleteTables();
                var obj = Yelp.CreateInstance();
                obj.ScrapeWebsite();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("goodbye");
            //repo.deleteTables();
        }
    }


}