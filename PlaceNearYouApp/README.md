# PlaceNearYouApp
ASP.NET MVC 5 web application prototype for searching restaurants near you

#### Why?
I Created this application prototype to demonstrate my understanding of creating apps using ASP.NET MVC 5

####What is it?
1. The application allows users to search for restaurants based on state and city values
2. The application will query restaurants based on distance using the bounding box principle

---

####How do I use it? 
1. In the locations field, start typing a state or city that you want to search
2. The autocomplete feature will pop up, select a state and city
3. Based on the values, you will see the restaurant results

---
####Core classes
- [Geo Location](https://github.com/deniscekic786/RestaurantsNearYou/blob/master/PlaceNearYouApp/PlaceNearYouApp/BLL/GeoLocation.cs)
- [Geo Location Helper](https://github.com/deniscekic786/RestaurantsNearYou/blob/master/PlaceNearYouApp/PlaceNearYouApp/Extentions/Helpers.cs)
- [Linq Query Box Distance](https://github.com/deniscekic786/RestaurantsNearYou/blob/master/PlaceNearYouApp/PlaceNearYouApp/DAL/RestaurantRepository.cs)

