using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantMVC.Models;
using RestaurantMVC.ViewModels;

namespace RestaurantMVC.Controllers
{
    public class FoodController : Controller
    {
        Uri base_address = new Uri("https://localhost:44322/api");
        HttpClient client;
        public FoodController()
        {
            client = new HttpClient();
            client.BaseAddress = base_address;
        }
        public IActionResult FoodItems(string Email)
        {

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Food/").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<Food> fl = JsonConvert.DeserializeObject<List<Food>>(data);
                FoodViewModel view = new FoodViewModel
                {
                    Email = Email,
                    foods = fl
                };
                return View(view);
            }
            return View();
        }
    }
}
