using OdeToFood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.ViewModel
{
    public class HomePageViewModel
    {
        public string TodayMessage { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }
    }
}
