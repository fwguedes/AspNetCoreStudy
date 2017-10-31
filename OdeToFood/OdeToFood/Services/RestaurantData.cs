using OdeToFood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Services
{

    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        Restaurant Add(Restaurant restaurant);
        void Commit();
    }

    public class SqlRestaurantData : IRestaurantData
    {
        private OdeToFoodContext _context;

        public SqlRestaurantData(OdeToFoodContext context)
        {
            _context = context;
        }

        public Restaurant Add(Restaurant restaurant)
        {
            _context.Add(restaurant);
            return restaurant;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants;
        }
    }

    public class InMemoryRestaurantData : IRestaurantData
        
    {

        static  InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant() { Id = 1, Name = "Coxinhas"},
                new Restaurant() { Id = 2, Name = "Japones"},
                new Restaurant() { Id = 3, Name = "Hamburgueria"}
            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
           return restaurants;
        }

        public Restaurant Get(int id)
        {
            return restaurants.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant restaurant)
        {
            restaurant.Id = restaurants.Max(r => r.Id) + 1;
            restaurants.Add(restaurant);
            return restaurant;
        }

        public void Commit()
        {
            // Nothing...
        }

        static List<Restaurant> restaurants;
    }
}
