using RestaurantMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantMVC.ViewModels
{
    public class FoodViewModel
    {
        public string Email { get; set; }
        public IEnumerable<Food> foods { get; set; }
    }
}
