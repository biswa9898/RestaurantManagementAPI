using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantMVC.Models;

namespace RestaurantMVC.Controllers
{
    public class OrdersController : Controller
    {
        Uri base_address = new Uri("https://localhost:44322/api");
        HttpClient client;
        public OrdersController()
        {
            client = new HttpClient();
            client.BaseAddress = base_address;
        }
        public IActionResult PlaceOrder(string UserId , int FoodId)
        {
            Orders order = new Orders { Email = UserId, FoodId = FoodId };
            string data = JsonConvert.SerializeObject(order);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/orders", content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("FoodItems","Food", new { Email = UserId });
            }
            return View();
        }

        public IActionResult MyOrders(string Email)
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/orders").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                IEnumerable<Orders> orders = JsonConvert.DeserializeObject<IEnumerable<Orders>>(data);
                orders = from o in orders where o.Email == Email select o;
                return View(orders);
            }
            return View();
        }
    }
}
