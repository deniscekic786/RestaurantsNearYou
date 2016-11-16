using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.DAL;

namespace WebScraper
{
    public class Yelp : Scraper
    {
        private readonly string baseUrl = "https://www.yelp.com/search?find_desc=Restaurants&find_loc={0},+{1}&start=0";
        private readonly string paginatorXpath = "//div[@class='arrange_unit page-option']/a[@class='available-number pagination-links_anchor']";
        private RestaurantRepo db = new RestaurantRepo();
        private bool PagesExist { get; set; }
        private Yelp(){}
        public static Yelp CreateInstance()
        {
            return new Yelp();
        }

        /// <summary>
        /// Calls a method to fetch urls based on query string parameters
        /// state and city. Loops through the collection of urls and if one of the 
        /// urls searched for returns false, I will retry new city values within the same
        /// state until it works.
        /// </summary>
        public void ScrapeWebsite()
        {
            var yelpBuilder = BuildYelpElements();
            var scraper = new Scraper();
            var locations = db.GetLocationsToSearchFor();
            foreach (var state in locations)
            {
                Console.WriteLine("Starting to search location: {0}, {1}", state.Key, state.Value);
                PagesExist = scraper.FetchPageUrls(string.Format(baseUrl, state.Key.Replace(" ", "+"), state.Value.Replace(" ", "+")), paginatorXpath, yelpBuilder);
                if (!PagesExist)
                {
                    Console.WriteLine("Could not find that location {0}, {1} from yelp, checking new location", state.Key, state.Value);
                    while (!PagesExist)
                    {
                        var newLocation = db.RetryNewLocation(state.Key);
                        Console.WriteLine("Trying {0}, {1} ", state.Key, newLocation);
                        PagesExist = scraper.FetchPageUrls(
                            string.Format(baseUrl, state.Key.Replace(" ", "+"), newLocation.Replace(" ", "+")),
                            paginatorXpath, yelpBuilder);
                    }
                }
                Console.WriteLine("Finished saving {0}, {1} in the database", state.Key, state.Value);
            }
        }

        /// <summary>
        /// builds the dictionary keys that will be populated
        /// with html nodes
        /// </summary>
        /// <returns></returns>
        private static YelpElementBuilder BuildYelpElements()
        {
            YelpElementBuilder yelpBuilder;
            ElementDirector director = new ElementDirector();
            yelpBuilder = new YelpElementBuilder();
            director.Construct(yelpBuilder);
            return yelpBuilder;
        }
    }
}
