using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PlaceNearYouApp.DAL;

namespace PlaceNearYouApp.Controllers
{
    public class SearchController : Controller
    {
        RestaurantRepository db = new RestaurantRepository();
        
        // GET: Search
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        // GET: Restaurant Search Results
        [HttpGet]
        public ActionResult Restaurants(int? page, string state, string city)
        {
            var restaurants = db.GetAllNearByRestaurants(state, city);
            ViewBag.State = state;
            ViewBag.City = city;
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(restaurants.ToPagedList(pageNumber, pageSize));
        }




        // GET: Locations
        [HttpGet]
        public ActionResult Locations()
        {
            var locations = db.GetLocationsToSearchFor();
            return Json(locations, JsonRequestBehavior.AllowGet);
        }



    }
}