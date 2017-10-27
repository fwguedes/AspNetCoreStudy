using OdeToFood.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.ViewModel
{
    public class RestaurantEditViewModel
    {
        [Required, MaxLength(20)]
        [Display(Name = "Trade Name")]
        public string Name { get; set; }
        public CuisineType Cuicine { get; set; }
    }
}
