using Microsoft.AspNetCore.Mvc;
using OdeToFood.Entities;
using OdeToFood.Services;
using OdeToFood.ViewModel;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;
        private IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData,IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }


        public IActionResult Index()
        {
            var model = new HomePageViewModel();
            model.Restaurants = _restaurantData.GetAll();
            model.TodayMessage = _greeter.Greet();

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var model = _restaurantData.Get(id);

            if(model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditViewModel restaurant)
        {
            if (ModelState.IsValid)
            {
                var newRestaurante = new Restaurant()
                {
                    Name = restaurant.Name,
                    Cuisine = restaurant.Cuicine
                };
                newRestaurante = _restaurantData.Add(newRestaurante);
                _restaurantData.Commit();
                return RedirectToAction("Detail", new { newRestaurante.Id });
            }

            return View();
           
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _restaurantData.Get(id);
            if(model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(int id, RestaurantEditViewModel model)
        {
            var restaurante = _restaurantData.Get(id);
            if (ModelState.IsValid)
            {
                restaurante.Cuisine = model.Cuicine;
                restaurante.Name = model.Name;
                _restaurantData.Commit();
                return RedirectToAction("Detail", new { id = restaurante.Id });
            }
            return View(restaurante);

        }


    }
}
