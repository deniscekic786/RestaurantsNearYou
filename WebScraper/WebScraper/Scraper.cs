using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WebScraper.App_Data;
using System.Net;
using System.Web;

namespace WebScraper
{

    public interface IScraper
    {
        bool FetchPageUrls(string baseUrl, string paginatorXpath, ElementBuilder builder);
        void GetNodes(ElementBuilder builder, string[] links);
    }
    public class Scraper : IScraper
    {
        /// <summary>
        /// Dictionary that will hold all values from html nodes
        /// </summary>
        private  Dictionary<string, List<string>> ScrapedStorage = new Dictionary<string, List<string>>();
       
  
        /// <summary>
        /// Gets all the page urls from the first page
        /// the value of all of the urls will be used to grab
        /// all the html pages
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="paginatorXpath"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public bool FetchPageUrls(string baseUrl, string paginatorXpath, ElementBuilder builder)
        {
            var document = new Document();
            document.StartPage(baseUrl);
            var doc = document.Pages[0];
            var links = doc.DocumentNode.SelectNodes(paginatorXpath);
            if (!LinksExists(paginatorXpath, doc)) return false;
            GetNodes(builder, links.Select(x => x.Attributes["href"].Value).ToArray());
            return true;
        }


        /// <summary>
        /// checks to see if the links exist when searching the document object
        /// if it does not exist I will have to try and pass new parameters
        /// from the calling object
        /// </summary>
        /// <param name="xPath"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static bool LinksExists(string xPath, HtmlDocument doc)
        {
            var links = doc.DocumentNode.SelectNodes(xPath);
            return links != null;
        }

        /// <summary>
        /// Grabbing all html documents based on previous fetched page links
        /// Setting the dictionary based on key names
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="links"></param>
        public void GetNodes(ElementBuilder builder, string[] links)
        {
            InitalizeKeys();
            var pages = new Document();
            pages.CreatePages(links);
            foreach (var doc in pages.Pages)
            {
                var root = doc.DocumentNode;
                var main = root.SelectSingleNode(builder.Element["Main"].xPaths);
                var dicval = builder.Element["Main"];
                var csddscsdcds = root.SelectSingleNode("//ul[@class='ylist ylist-bordered search-results']");
                if (main != null)
                {
                    var name = main.SelectNodes(builder.Element["Name"].xPaths).Select(x => x.InnerText.Trim().Replace("\n","")).ToList();
                    ScrapedStorage["Name"].AddRange(name);
              
                    var category = main.SelectNodes(builder.Element["Category"].xPaths).Where(x => x.InnerText != null).Select(x => x.InnerText.Replace(" ", "").ToLower().Replace("\n", "")).ToList();
                    ScrapedStorage["Category"].AddRange(category);

                    var imageAnchor = main.SelectNodes(builder.Element["ImageAnchor"].xPaths).Where(x => x.Attributes["href"].Value != null).Select(x => x.Attributes["href"].Value).ToList();
                    ScrapedStorage["ImageAnchor"].AddRange(imageAnchor);

                    var imageLink = main.SelectNodes(builder.Element["ImageLink"].xPaths).Where(x => x.Attributes["src"].Value != null).Select(x => x.Attributes["src"].Value).ToList();
                    ScrapedStorage["ImageLink"].AddRange(imageLink);

                    var address = main.SelectNodes(builder.Element["Address"].xPaths).Where(x => x.InnerHtml != null).Select(x => x.InnerHtml.Trim().Replace("\n", "")).ToList();
                    ScrapedStorage["Address"].AddRange(address);

                    var phone = main.SelectNodes(builder.Element["Phone"].xPaths).Where(x => x.InnerText != null).Select(x => x.InnerText.Trim().Replace("\n", "")).ToList();
                    ScrapedStorage["Phone"].AddRange(phone);
                }
            }
            IterateNodes(ScrapedStorage);
        }


        /// <summary>
        /// Iterates the dictionary that holds all of the scraped values
        /// saves the values into the database 
        /// </summary>
        /// <param name="scrapedStorage"></param>
        public void IterateNodes(Dictionary<string,List<string>>  scrapedStorage)
        {

            var newAddress = scrapedStorage["Address"].Select(x => new
                {
                    SplitAddress = x.Split(new string[] {"<br>"}, StringSplitOptions.None)
                })
                .Where(x => x.SplitAddress.Length == 2)
                .Select(
                    x =>
                        new
                        {
                            CityStateZip = x.SplitAddress[1],
                            Streets = x.SplitAddress[0],
                            PostalCode = ScraperExtentions.ExtractPostalCode(x.SplitAddress[1])
                        })
                .ToList();
            ScrapedStorage["CityStateZip"].AddRange(newAddress.Select(x=>x.CityStateZip));
            ScrapedStorage["Streets"].AddRange(newAddress.Select(x=>x.Streets));
            ScrapedStorage["PostalCode"].AddRange(newAddress.Select(x => x.PostalCode));

            var results = ScrapedStorage["Name"].Zip8( ScrapedStorage["Category"],
                ScrapedStorage["ImageAnchor"], ScrapedStorage["ImageLink"],ScrapedStorage["Phone"],
                ScrapedStorage["CityStateZip"], ScrapedStorage["Streets"],ScrapedStorage["PostalCode"],
                (n1, n2, n3, n4, n5, n6, n7, n8) => new Restaurant
                {Name = n1, Category = n2,ImageReference = n3, ImageSource = n4,PhoneNumber = n5,
                    Address = new Address
                    { CityStatePostal = n6, Street = n7,PostalCode = n8}
                });
            var repo = new RestaurantRepo();
            repo.InsertRestaurants(results);
            EmptyStorage(scrapedStorage);
        }


        /// <summary>
        /// Empties the dictionary holding scraped values 
        /// </summary>
        /// <param name="scrapedStorage"></param>
        private void EmptyStorage(Dictionary<string, List<string>> scrapedStorage)
        {
            scrapedStorage.Clear();
        }

        /// <summary>
        /// creates the keys for the dictionary so that I could
        /// call the AddRange method without worrying about duplicate keys
        /// </summary>
        private void InitalizeKeys()
        {
            ScrapedStorage.Add("Name", new List<string>() { });
            ScrapedStorage.Add("Category", new List<string>() { });
            ScrapedStorage.Add("ImageAnchor", new List<string>() { });
            ScrapedStorage.Add("ImageLink", new List<string>() { });
            ScrapedStorage.Add("Address", new List<string>() { });
            ScrapedStorage.Add("CityStateZip", new List<string>() { });
            ScrapedStorage.Add("PostalCode", new List<string>() { });
            ScrapedStorage.Add("Phone", new List<string>() { });
            ScrapedStorage.Add("Streets", new List<string>() { });
        }

    }
}
