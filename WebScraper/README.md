# WebScraper
Console Application for Scraping Websites

#### Why?
1. I Created this application prototype to scrape restaurant data from yelp
2. The scraped data gets stored in a database for my web application to query


####What is it?
The application scrapes yelp restaurants based on location and stores the results in a database

---

####How it works? 
1. Run the console application and it will form a collection of state and city values
2. The state and city values are then used to create a query string with a full url
3. The url is then used to fetch the first page
4. The first page is used to scrape all result urls in the paginator
5. The paginator results are used to fetch and scrape a collection of HTML documents

####How to use it?
1. The database is already pre-populated with results
2. If you want to start from scratch you can uncomment the delete method
3. If deleted, please re-run the console application or else the web application will have no results to display

---
####Core classes
- [Scraper](https://github.com/deniscekic786/RestaurantsNearYou/blob/master/WebScraper/WebScraper/Scraper.cs)
- [Html document fetcher] (https://github.com/deniscekic786/RestaurantsNearYou/blob/master/WebScraper/WebScraper/Document.cs)
- [Linq Zip method extention] (https://github.com/deniscekic786/RestaurantsNearYou/blob/master/WebScraper/WebScraper/ZipGeneric.cs)



